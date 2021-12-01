using System.Windows;

namespace StudentProjects
{
    public class ModalHandler
    {
        private static Window _mainWindow;

        public static void SetMainWindow(Window window)
        {
            if (_mainWindow != null)
            {
                ErrorsHandler.Throw(ErrorType.MainWindowAlreadyDefined);

                return;
            }

            _mainWindow = window;
        }

        public static void Open(Window window)
        {
            if (window == _mainWindow)
                return;

            window.Owner = _mainWindow;
            window.Show();
        }
    }
}