﻿/* BookManager - (C) 2016 Premysl Fara 
 
BookManager is available under the zlib license:

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace BookManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;


    public sealed class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        // Flag used to prevent OnCollectionChanged from firing during a bulk operation like Add(IEnumerable<T>) and Clear()
        private bool _suppressCollectionChanged = false;

        /// Overridden so that we may manually call registered handlers and differentiate between those that do and don't require Action.Reset args.
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public ExtendedObservableCollection() : base() { }
        public ExtendedObservableCollection(IEnumerable<T> data) : base(data) { }


        #region Event Handlers

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_suppressCollectionChanged) return;

            base.OnCollectionChanged(e);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, e);
            }
        }

        /// CollectionViews raise an error when they are passed a NotifyCollectionChangedEventArgs that indicates more than
        /// one element has been added or removed. They prefer to receive a "Action=Reset" notification, but this is not suitable
        /// for applications in code, so we actually check the type we're notifying on and pass a customized event args.
        private void OnCollectionChangedMultiItem(NotifyCollectionChangedEventArgs e)
        {
            var handlers = this.CollectionChanged;
            if (handlers == null) return;

            foreach (NotifyCollectionChangedEventHandler handler in handlers.GetInvocationList())
            {
                handler(this, !(handler.Target is ICollectionView) ? e : new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        #endregion


        #region Extended Collection Methods

        protected override void ClearItems()
        {
            if (this.Count == 0) return;

            var removed = new List<T>(this);

            _suppressCollectionChanged = true;

            base.ClearItems();

            _suppressCollectionChanged = false;

            OnCollectionChangedMultiItem(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
        }


        public void Add(IEnumerable<T> toAdd)
        {
            if (this == toAdd)
            {
                throw new Exception("Invalid operation. This would result in iterating over a collection as it is being modified.");
            }

            _suppressCollectionChanged = true;

            foreach (T item in toAdd)
            {
                Add(item);
            }

            _suppressCollectionChanged = false;

            OnCollectionChangedMultiItem(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(toAdd)));
        }


        public void Remove(IEnumerable<T> toRemove)
        {
            if (this == toRemove)
            {
                throw new Exception("Invalid operation. This would result in iterating over a collection as it is being modified.");
            }

            _suppressCollectionChanged = true;

            foreach (T item in toRemove)
            {
                Remove(item);
            }

            _suppressCollectionChanged = false;

            OnCollectionChangedMultiItem(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<T>(toRemove)));
        }

        /// <summary>
        /// Synchronizes this collection with an other collection.
        /// </summary>
        /// <param name="collection">A target collection (we will by synchronized with).</param>
        /// <param name="collectionSynchronizer">A collectionSynchronizer.</param>
        public void SynchronizeWith(Collection<T> collection, ICollectionSynchronizer<T> collectionSynchronizer)
        {
            if (this == collection)
            {
                throw new Exception("Invalid operation. This would result in iterating over a collection as it is being modified.");
            }

            _suppressCollectionChanged = true;

            // Initialize iterators and accumulators
            var thisIndex = 0;
            var thatIndex = 0;

            IList<Insert<T>> inserts = new List<Insert<T>>();
            IList<int> deletes = new List<int>();
            IList<Update<T>> updates = new List<Update<T>>();

            // While there are unexamined elements in either the subject or target list:
            while (thisIndex < this.Count || thatIndex < collection.Count)
            {
                // If the target list is exhausted,
                // delete the current element from the subject list
                if (thatIndex >= collection.Count)
                {
                    deletes.Add(thisIndex);
                    thisIndex += 1;
                }

                // O/w, if the subject list is exhausted,
                // insert the current element from the target list
                else if (thisIndex >= this.Count)
                {
                    inserts.Add(new Insert<T> { Index = thatIndex, Entity = collection[thatIndex] });
                    thatIndex += 1;
                }

                // O/w, if the current subject element precedes the current target element,
                // delete the current subject element.
                //else if (a[x] < b[y])
                else if (collectionSynchronizer.GetOrder(this[thisIndex], collection[thatIndex]) < 0)
                {
                    deletes.Add(thisIndex);
                    thisIndex += 1;
                }

                // O/w, if the current subject element follows the current target element,
                // insert the current target element.
                //else if (a[x] > b[y])
                else if (collectionSynchronizer.GetOrder(this[thisIndex], collection[thatIndex]) > 0)
                {
                    inserts.Add(new Insert<T> { Index = thatIndex, Entity = collection[thatIndex] });
                    thatIndex += 1;
                }

                // O/w the current elements match; consider the next pair
                else
                {
                    if (collectionSynchronizer.NeedsUpdate(this[thisIndex], collection[thatIndex]))
                    {
                        updates.Add(new Update<T> { Index = thatIndex, Entity = collection[thatIndex] });
                    }

                    thisIndex += 1;
                    thatIndex += 1;
                }
            }

            // Do all deletes, ordered from the highest to the lowest index.
            var deletedList = new List<T>();
            for (var i = deletes.Count - 1; i >= 0; i--)
            {
                deletedList.Add(this[deletes[i]]);

                this.RemoveAt(deletes[i]);
            }

            // Do all inserts, ordered from the lowest to the highest index.
            var insertedList = new List<T>();
            foreach (var insert in inserts)
            {
                insertedList.Add(insert.Entity);

                this.Insert(insert.Index, insert.Entity);
            }

            // Do all updates, ordered from the lowest to the highest index.
            var updatedListOld = new List<T>();
            var updatedListNew = new List<T>();
            foreach (var update in updates)
            {
                updatedListOld.Add(this[update.Index]);
                updatedListNew.Add(update.Entity);

                this[update.Index] = update.Entity;
            }

            _suppressCollectionChanged = false;

            // Inform about deleted items.
            if (deletedList.Count > 0)
            {
                OnCollectionChangedMultiItem(
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        deletedList));
            }

            // Inform about inserted items.
            if (insertedList.Count > 0)
            {
                OnCollectionChangedMultiItem(
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        insertedList));
            }

            // Inform about updated items.
            if (updatedListOld.Count > 0)
            {
                OnCollectionChangedMultiItem(
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        updatedListOld, updatedListNew, -1));
            }
        }

        #endregion
    }

    /// <summary>
    /// Compares two entities from twho collections to find their natural order
    /// and whether they are the same or not.
    /// </summary>
    /// <typeparam name="T">A type of objects in both collections.</typeparam>
    public interface ICollectionSynchronizer<in T>
    {
        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        int GetOrder(T a, T b);

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        bool NeedsUpdate(T a, T b);
    }

    /// <summary>
    /// Describes an insert operation.
    /// </summary>
    /// <typeparam name="T">A type of an inserted object.</typeparam>
    internal class Insert<T>
    {
        public int Index { get; set; }
        public T Entity { get; set; }
    }

    /// <summary>
    /// Describes an update operation.
    /// </summary>
    /// <typeparam name="T">A type of an updated object.</typeparam>
    internal class Update<T>
    {
        public int Index { get; set; }
        public T Entity { get; set; }
    }
}
