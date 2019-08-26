﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace BackupDataBase
{
	public partial class Configuracao : Form
	{
		ParametrosAppConfig value = new ParametrosAppConfig();
		ParametrosDiretorio diretorio = new ParametrosDiretorio();
		ParametrosSpacenetConfig parametros = new ParametrosSpacenetConfig();

		public Configuracao()
		{
			InitializeComponent();
			BotaoSalvarConfig();
			ImagemDeAlterarConfiguracao();
			DesabilitarConfiguracoes();
			CarregarAppConfig();
			EscolherBancoDeDados();
		}

		public void AbrirTelaPrincipal()
		{
			Principal principal = new Principal
			{
				Visible = true
			};
		}

		public void FecharTelaConfiguracao()
		{
			this.Close();
		}


		public void AtualizarAppConfig()
		{
			value.InstanciaBancoDeDados = textBox1.Text;
			value.BancoDeDados = textBox2.Text;
			value.BancoDeDadosSac = textBox3.Text;
			value.DiretorioSpaceNet = textBox4.Text;
			value.DiretorioFile = textBox5.Text;
			value.SalvarParametrosAppConfig();
		}

		public void CarregarAppConfig()
		{
			textBox1.Text = value.InstanciaBancoDeDados;
			textBox2.Text = value.BancoDeDados;
			textBox3.Text = value.BancoDeDadosSac;
			textBox4.Text = value.DiretorioSpaceNet;
			textBox5.Text = value.DiretorioFile;
		}

		public void DesabilitarConfiguracoes()
		{
			textBox1.Enabled = false;
			textBox2.Enabled = false;
			textBox3.Enabled = false;
			textBox4.Enabled = false;
			textBox5.Enabled = false;
		}

		public void HabilitarAlterarConfiguracoes()
		{
			textBox1.Enabled = true;
			textBox2.Enabled = true;
			textBox3.Enabled = true;
			textBox4.Enabled = true;
			textBox5.Enabled = true;
			btnSalvar.Enabled = false;
		}
		
		private void Configuracao_Load(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			BotaoAlterarConfig(false);
			BotaoSalvarConfig(true);
			BotaoSalvar(false);
			ImagemDeAlterarConfiguracao(true);
			HabilitarAlterarConfiguracoes();
		}

		public void EscolherBancoDeDados()
		{
			if (diretorio.DiretorioDataBase(value.DiretorioFile))
			{
				DirectoryInfo diretorioDB = new DirectoryInfo(value.DiretorioFile);
				FileInfo[] Arquivos = diretorioDB.GetFiles();
				string valor;
				foreach (FileInfo arquivo in Arquivos)
				{
					valor = arquivo.Name;
					if (valor.EndsWith("mdf"))
					{
						comboBox1.Items.Add(valor.Substring(0, valor.Length - 4));
					}
				}
			}
			else
			{
				value.BancoDeDados = "";
				value.SalvarParametrosAppConfig();
				value.RefreshConfiguracoesAppConfig();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (diretorio.BancoDeDadosSelecionado(comboBox1.Text))
			{
				textBox2.Text = comboBox1.Text;
				AtualizarAppConfig();
				value.SalvarParametrosAppConfig();
				value.RefreshConfiguracoesAppConfig();
				parametros.SpacenetConfig(textBox4.Text, textBox2.Text, textBox1.Text, textBox3.Text);
				FecharTelaConfiguracao();
				AbrirTelaPrincipal();
			}
		}

		private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			value.RefreshConfiguracoesAppConfig();
			FecharTelaConfiguracao();
			AbrirTelaPrincipal();
		
		}

		private void button2_Click(object sender, EventArgs e)
		{
			menuStrip1.Enabled = false;
			BotaoSalvar(true);
			BotaoAlterarConfig(true);
			BotaoSalvarConfig(false);
			ImagemDeAlterarConfiguracao();
			AtualizarAppConfig();
			value.SalvarParametrosAppConfig();
			value.RefreshConfiguracoesAppConfig();
			comboBox1.Items.Clear();
			EscolherBancoDeDados();
			DesabilitarConfiguracoes();
			

		}

		public void BotaoSalvarConfig(bool check = false)
		{
			btnSalvarConfig.Enabled = check;
		}

		public void BotaoAlterarConfig(bool check = true)
		{
			btnAlterarConfig.Enabled = check;
		}

		public void BotaoSalvar(bool check = false)
		{
			btnSalvar.Enabled = check;
		}

		public void ImagemDeAlterarConfiguracao(bool check = false)
		{
			AlterarConfigImage.Visible = check;
		}


	}
}
