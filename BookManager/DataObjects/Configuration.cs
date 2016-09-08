/* BookManager - (C) 2016 Premysl Fara 
 
Helpers are available under the zlib license :

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

namespace BookManager.DataObjects
{
    using SimpleDb.Shared;


    /// <summary>
    /// Application configuration.
    /// </summary>
    [DbTable("Configuration")]
    public class Configuration : AIdDataObject
    {
        #region constans

        /// <summary>
        /// The default if the main app. window was maximized.
        /// </summary>
        public const bool DefaultMaximized = false;

        /// <summary>
        /// The default position of the top endge of the main app. window.
        /// </summary>
        public const int DefaultTop = 0;

        /// <summary>
        /// The default position of the left endge of the main app. window.
        /// </summary>
        public const int DefaultLeft = 0;

        /// <summary>
        /// The default width of the main app. window.
        /// </summary>
        public const int DefaultWidth = 1024;

        /// <summary>
        /// The default height of the main app. window.
        /// </summary>
        public const int DefaultHeight = 768;

        #endregion


        #region fields

        private bool _isMaximized;
        private int _top;
        private int _left;
        private int _width;
        private int _height;

        #endregion


        #region ctor

        public Configuration()
        {
            IsMaximized = DefaultMaximized;
            Top = DefaultTop;
            Left = DefaultLeft;
            Width = DefaultWidth;
            Height = DefaultHeight;
        }

        #endregion


        #region properties

        /// <summary>
        /// Gets or sets if the main app. window was maximized.
        /// </summary>
        [DbColumn("IsMaximized")]
        public bool IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                if (_isMaximized != value)
                {
                    _isMaximized = value;
                    OnPropertyChanged("IsMaximized");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the top edge of the main app. window.
        /// </summary>
        [DbColumn("Top")]
        public int Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    OnPropertyChanged("Top");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the left edge of the main app. window.
        /// </summary>
        [DbColumn("Left")]
        public int Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    OnPropertyChanged("Left");
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the main app. window.
        /// </summary>
        [DbColumn("Width")]
        public int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the main app. window.
        /// </summary>
        [DbColumn("Height")]
        public int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        #endregion
    }
}
