using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Resources;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.VisualStudio.CommandBars;

namespace ResetTabOrder
{
    /// <summary>用於實作增益集的物件。</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        /// <summary>針對增益集物件實作建構函式。請將初始化程式碼置於此方法中。</summary>
        public Connect()
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnConnection 方法。會收到正在載入增益集的告知。</summary>
        /// <param term='application'>主應用程式的根物件。</param>
        /// <param term='connectMode'>描述如何載入增益集。</param>
        /// <param term='addInInst'>代表此增益集的物件。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                object[] contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)_applicationObject.Commands;
                string toolsMenuName = "Tools";

                //將命令放在 [工具] 功能表上。
                //尋找 MenuBar 命令列，其為包含所有主要功能表項目的最上層命令列:
                Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["MenuBar"];

                //在 MenuBar 命令列上尋找 Tools 命令列:
                CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

                //  如果您要加入多個由增益集處理的命令，可以複製此 try/catch 區塊，
                //  但務必同時更新 QueryStatus/Exec 方法，以包含新命令的名稱。
                try
                {
                    //將命令加入至 Commands 集合:
                    Command command = commands.AddNamedCommand2(_addInInstance, "ResetTabOrder", "ResetTabOrder", "Executes the command for ResetTabOrder", true, 6743, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled, (int)vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton);

                    //將命令的控制項加入至 [工具] 功能表:
                    if ((command != null) && (toolsPopup != null))
                    {
                        command.AddControl(toolsPopup.CommandBar, 1);
                    }
                }
                catch (System.ArgumentException)
                {
                    //  如果執行到此處，則發生例外狀況的原因可能是因為已經有
                    //  相同名稱的命令。如果是這樣，就不需要重新建立命令，
                    //  並且可以安全無虞地忽略此例外狀況。
                }
            }
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnDisconnection 方法。會收到正在卸載增益集的告知。</summary>
        /// <param term='disconnectMode'>描述如何卸載增益集。</param>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnAddInsUpdate 方法。增益集的集合變更時會收到告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnStartupComplete 方法。會收到主應用程式載入完成的告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>實作 IDTExtensibility2 介面的 OnBeginShutdown 方法。會收到正在卸載主應用程式的告知。</summary>
        /// <param term='custom'>主應用程式專屬參數的陣列。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

        /// <summary>實作 IDTCommandTarget 介面的 QueryStatus 方法。這會在命令的可用性更新時呼叫</summary>
        /// <param term='commandName'>要判斷其狀態的命令名稱。</param>
        /// <param term='neededText'>命令所需的文字。</param>
        /// <param term='status'>命令在使用者介面中的狀態。</param>
        /// <param term='commandText'>neededText 參數要求的文字。</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "ResetTabOrder.Connect.ResetTabOrder")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                    return;
                }
            }
        }

        /// <summary>實作 IDTCommandTarget 介面的 Exec 方法。這會在叫用命令時呼叫。</summary>
        /// <param term='commandName'>要執行的命令名稱。</param>
        /// <param term='executeOption'>描述如何執行命令。</param>
        /// <param term='varIn'>從呼叫端傳遞至命令處理常式的參數。</param>
        /// <param term='varOut'>從命令處理常式傳遞至呼叫端的參數。</param>
        /// <param term='handled'>通知呼叫端是否已處理命令。</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == "ResetTabOrder.Connect.ResetTabOrder")
                {
                    ResetTabOrder.DoWork(_applicationObject.ActiveDocument);
                    handled = true;
                    return;
                }
            }
        }

        private DTE2 _applicationObject;
        private AddIn _addInInstance;
    }
}