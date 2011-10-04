﻿using System;
using System.Windows.Forms;
using SSHTunnelManager.Domain;
using SSHTunnelManagerGUI.Properties;

namespace SSHTunnelManagerGUI.Forms
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog(PuttyProfile puttyProfile)
        {
            InitializeComponent();

            PuttyProfile = puttyProfile;

            checkGroupBoxAutoRestart.Checked = Settings.Default.Config_RestartEnabled;
            numericUpDownRestartDelay.Value = Settings.Default.Config_RestartDelay;
            numericUpDownMaxAttemptsCount.Value = Settings.Default.Config_MaxAttemptsCount;
            checkBoxTraceDebug.Checked = Settings.Default.Config_TraceDebug;
            if (PuttyProfile != null)
            {
                checkBoxLocalPortAcceptAll.Checked = PuttyProfile.LocalPortAcceptAll;
                checkBoxRemotePortAcceptAll.Checked = PuttyProfile.RemotePortAcceptAll;
            } else
            {
                checkBoxLocalPortAcceptAll.Enabled = false;
                checkBoxRemotePortAcceptAll.Enabled = false;
            }
        }

        public PuttyProfile PuttyProfile { get; private set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Config_RestartEnabled = checkGroupBoxAutoRestart.Checked;
            Settings.Default.Config_RestartDelay = (int)numericUpDownRestartDelay.Value;
            Settings.Default.Config_MaxAttemptsCount = (int)numericUpDownMaxAttemptsCount.Value;
            Settings.Default.Config_TraceDebug = checkBoxTraceDebug.Checked;
            Settings.Default.Save();
            if (PuttyProfile != null)
            {
                PuttyProfile.LocalPortAcceptAll = checkBoxLocalPortAcceptAll.Checked;
                PuttyProfile.RemotePortAcceptAll = checkBoxRemotePortAcceptAll.Checked;
                PuttyProfile.Save();
            }
        }
    }
}