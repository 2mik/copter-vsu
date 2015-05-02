﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scada.Comm.KP.AeroQuad
{
    /// <summary>
    /// Форма управления и сервиса
    /// </summary>
    public partial class FrmControl : Form
    {
        private string cmdDir;

        /// <summary>
        /// Конструктор, ограничивающий создание формы без параметров
        /// </summary>
        private FrmControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отобразить форму модально
        /// </summary>
        public static void ShowDialog(string cmdDir)
        {
            FrmControl frmControl = new FrmControl();
            frmControl.cmdDir = cmdDir;
            frmControl.ShowDialog();
        }

        private void btnSend_i_Click(object sender, EventArgs e)
        {
            KPLogic.Command cmd = new KPLogic.Command(KPLogic.CmdType.Binary);
            cmd.CmdNum = 1;
            cmd.CmdData = new byte[] { 0x69 }; // символ i
            string msg;

            if (KPUtils.SaveCmd(cmdDir, "KpAeroQuad", cmd, out msg))
                ScadaUtils.ShowInfo(msg);
            else
                ScadaUtils.ShowError(msg);
        }

        private void btnConvertToCsv_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileFormats.ConvertFileToCsv(openFileDialog.FileName,
                        Path.ChangeExtension(openFileDialog.FileName, "csv"));
                    ScadaUtils.ShowInfo("Конвертирование выполнено успешно.");
                }
                catch (Exception ex)
                {
                    ScadaUtils.ShowError(ex.Message);
                }
            }
        }
    }
}
