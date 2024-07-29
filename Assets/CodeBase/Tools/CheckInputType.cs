using UnityEngine;

namespace CodeBase.Tools
{
    public class CheckInputType
    {
        public bool Mobile => IsMobileDevice();

        private bool testInIspector = false;
        private bool mobile = false;
        private int firstCheckMobile = 0;

        public CheckInputType() =>
            Application.ExternalEval("checkForTouchDevice()");

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();

#endif
        private bool IsMobileDevice()
        {
            if (firstCheckMobile == 1)
                return mobile;
            firstCheckMobile = 1;


            var isMobile = false;

#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif

            if (isMobile)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mobile = true;
                return true;
            }
            else
            {
                if (testInIspector)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                mobile = testInIspector;
                return testInIspector;
            }
        }
    }
}