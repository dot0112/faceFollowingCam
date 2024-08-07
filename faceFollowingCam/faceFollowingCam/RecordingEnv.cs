using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faceFollowingCam
{
    internal static class RecordingEnv
    {
        private static string path = "C:\\Users\\deu\\Desktop";
        private static string name = "result";

        public static event EventHandler PathChanged;
        public static event EventHandler NameChanged;

        public static string Path
        {
            get => path;
            set
            {
                if (path != value)
                {
                    path = value;
                    PathChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string Name
        {
            get => name;
            set
            {
                if (Name != value)
                {
                    name = value;
                    NameChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
