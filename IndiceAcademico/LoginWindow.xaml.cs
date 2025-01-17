﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace IndiceAcademico
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{

		private void Button_Click(object sender, RoutedEventArgs e)
        {
			WindowState = WindowState.Minimized;
        }

		public static string filepathUser = "BaseUsuario.data";

		public LoginWindow()
		{
			InitializeComponent();

			if (!File.Exists(filepathUser))
			{
				string[] admin = { "A,admin,0000" };
				File.AppendAllLines(filepathUser, admin);
			}
				
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			bool userFound = false;
			if (inputUsuario.Text != "" && inputContrasena.Password != "")
			{
				foreach (var line in File.ReadAllLines(filepathUser))
				{
					string[] data = line.Split(',');

					if (data.Length == 3)
					{
						if (data[0] == "P" && inputUsuario.Text == data[1] && inputContrasena.Password == data[2])
						{
							userFound = true;
							ProfesorMainWindow window = new ProfesorMainWindow("P," + inputUsuario.Text + "," + inputContrasena.Password);
                            window.DisableAgregarAsignatura();
							window.Show();
							Close();
						}

						if (data[0] == "E" && inputUsuario.Text == data[1] && inputContrasena.Password == data[2])
						{
							userFound = true;
							EstudianteMainWindow window = new EstudianteMainWindow("E," + inputUsuario.Text + "," + inputContrasena.Password);
                            window.DisableRanking();
							window.Show();
							Close();
						}

						if (data[0] == "A" && inputUsuario.Text == data[1] && inputContrasena.Password == data[2])
						{
							userFound = true;
							MainWindow window = new MainWindow();
                            window.DisableAgregarCalificacion();
							window.Show();
							Close();
						}

					}

				}
			}

			if (!userFound)
				MessageBox.Show("Usuario y contraseña invalidos");
		}


	}
}
