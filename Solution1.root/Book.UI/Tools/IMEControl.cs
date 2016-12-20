using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Tools
{
    public class IMEControl : Object
    {
        public  static int IMECurIndex = 0;
        public static InputLanguage inputLanguage = null;


        public static void IMEEnter(object sender, EventArgs e)
        {
            //若当前输入法 不为默认输入法  设置输入法为最后一次输入法
            if (InputLanguage.CurrentInputLanguage.LayoutName != InputLanguage.DefaultInputLanguage.LayoutName)
                IMECurIndex = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[IMECurIndex];
        }

        //public static void IMELeave(object sender, EventArgs e)
        //{
        //    IMECurIndex = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);
        //    //若当前输入法 不为默认输入法  设置输入法为最后一次输入法
        //    if (InputLanguage.CurrentInputLanguage.LayoutName != InputLanguage.DefaultInputLanguage.LayoutName)
        //        IMECurIndex = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);
        //    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[IMECurIndex];
        //}

        //public static void IMEClose(object sender, EventArgs e)
        //{
        //    //恢复当前输入法  
        //    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[IMECurIndex];

        //}

        //输入法控制方法。。在窗体装的时候调用该方法，并将需要使用输入法的控制，列入其中即可！不需要什么额外的设置！ 
        public static void IMECtrl(Control[] UseIMEControls)
        {
            //为指定控件添加事件 
            foreach (Control UseIMECtl in UseIMEControls)
            {
                UseIMECtl.Leave += new EventHandler(IMEEnter);
               //UseIMECtl.Leave += new EventHandler(IMEOpen);
              
            }
        }
        public static void IMECtrl(Control[] UseIMEControls,DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit[] spins,DevExpress.XtraEditors.Repository.RepositoryItemTextEdit[] textEdits)
        {
            //为指定控件添加事件 

            //gridview 外控件
            foreach (Control UseIMECtl in UseIMEControls)
            {
                UseIMECtl.Enter += new EventHandler(IMEEnter);
                UseIMECtl.Leave += new EventHandler(IMEEnter);

            }

            //gridview 内数字控件
            foreach (DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spin in spins)
            {   

                spin.Enter += new EventHandler(NuControl);
            }
            //gridview 内文本控件
            foreach (DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEdit in textEdits)
            {
                textEdit.Enter += new EventHandler(IMEEnter);
                textEdit.Leave += new EventHandler(IMEEnter);
                //UseIMECtl.Leave += new EventHandler(IMEOpen);
            }
        }
        public static void NuControl(object sender, EventArgs e)
        {
            //恢复输入状态
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;

        }
       
   
    }
}
