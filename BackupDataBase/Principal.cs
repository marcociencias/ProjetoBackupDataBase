using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupDataBase
{
	public partial class Principal : Form
	{
		public Principal()
		{
			InitializeComponent();
			DataBase();
			ValidarRegistro();
			Inicializacao();
		}

		ParametrosDiretorio diretorio = new ParametrosDiretorio();
		ParametrosAppConfig value = new ParametrosAppConfig();
		Configuracao configuracao = new Configuracao();
		ParametrosRegedit regedit = new ParametrosRegedit();

		System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

		public void ValidarRegistro()
		{
			regedit.Regedit(value.DiretorioSpaceNet);
		}

		public void Inicializacao()
		{
			if(diretorio.DiretorioComunicador(value.DiretorioSpaceNet))
			{
				btnAbrirSpacenetIt.Enabled = false;
				return;
			}
		}

		public void DataBase()
		{
			value.SalvarParametrosAppConfig();
			value.RefreshConfiguracoesAppConfig();
			dataBaseTextBox.Text = value.BancoDeDados;
		}


		private void button1_Click(object sender, EventArgs e)
		{
			if (diretorio.DiretorioSpacenetIt(value.DiretorioSpaceNet))
			{
				pProcess.StartInfo.FileName = ($@"{value.DiretorioSpaceNet}\\spacenetIt.exe");
				pProcess.Start();
			}

		}

		private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Visible = false;
			configuracao.Visible = true;
			
		}

		private void sairToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Deseja realmente sair ?", "NEPOS BACKUP - SAIR", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
			{
				this.Close();
				Environment.Exit(0);
			}
		}
	}
}
