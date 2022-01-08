using System;
using System.Linq;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						Console.WriteLine("Opção Invalida!");
						break;
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			
			int indiceSerie = EntradaId();


			// confirmação se o usuario realmente deseja excluir
			Console.WriteLine($"Deseja mesmo excluir a serie {repositorio.RetornaPorId(indiceSerie).retornaTitulo()} (S/N)");

			if( Console.ReadLine().ToUpper() == "S")
				repositorio.Exclui(indiceSerie);

			
		}

        private static void VisualizarSerie()
		{
			int indiceSerie = EntradaId();
			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);	
		}

        private static void AtualizarSerie()
		{
			int indiceSerie = EntradaId();

			int entradaGenero = EntradaGenero();

			string entradaTitulo = EntradaTitulo();

			int entradaAno = EntradaAno();

			string entradaDescricao = EntradaDescricao();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}

		 
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries"); 

			var lista = repositorio.Lista().Where(x => !x.retornaExcluido()).ToList(); // Retorna apenas as series não excluidas;

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{ 
				Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			int entradaGenero = EntradaGenero();

			string entradaTitulo = EntradaTitulo();

			int entradaAno = EntradaAno();

			string entradaDescricao = EntradaDescricao();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

		private static int EntradaGenero() // Pede para o usuario digitar o gênero e faz a validação
		{
			int entradaGenero;
			while(true){
				foreach (int i in Enum.GetValues(typeof(Genero)))
				{
					Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
				}

				try{
					Console.Write("Digite o gênero entre as opções acima: ");
					entradaGenero = int.Parse(Console.ReadLine());
					if(entradaGenero < 0 || entradaGenero > 13)
						throw new Exception();
					
					return entradaGenero;
				} 
				catch
				{
					Console.WriteLine();
					Console.WriteLine("Opção invalida");
					Console.WriteLine();
				}
			}
		}

		private static int EntradaId()	// Pede para o usuario digitar o Id e faz a validação
		{
			while(true){
				Console.Write("Digite o id da série: ");

				try
				{
					int indiceSerie = int.Parse(Console.ReadLine());

					var serie = repositorio.RetornaPorId(indiceSerie);

					if(serie.retornaExcluido())
						throw new Exception();

					return indiceSerie;
					
				}
				catch
				{
					Console.WriteLine();
					Console.WriteLine("Id Invalido");
					Console.WriteLine();
				}
			}

		}

		private static string EntradaTitulo()	// Pede para o usuario digitar Titulo e faz a validação
		{
			while(true)
			{
				Console.WriteLine("Digite o Título da Série: ");
				string EntradaTitulo = Console.ReadLine();

				if(EntradaTitulo.Trim() != "")
					return EntradaTitulo;
				
				Console.WriteLine();
				Console.WriteLine("Titulo invalido!");
				Console.WriteLine();

			}
		}

		private static int EntradaAno()		// Pede para o usuario digitar o Ano e faz a validação
		{
			while(true)
			{
				try
				{
					Console.WriteLine("Digite o Ano de Início da Série: ");
					int entradaAno = int.Parse(Console.ReadLine());

					if(entradaAno <= int.Parse(DateTime.Now.ToString("yyyy")))
					{
						return entradaAno;
					}
				}
				catch
				{
					Console.WriteLine();					
					Console.WriteLine("Ano ivalido");
					Console.WriteLine();					
				}
			}
		}

		private static string EntradaDescricao()	// Pede para o usuario digitar a descrição e faz a validação
		{
			while(true)
			{
				Console.WriteLine("Digite a descrição da Série: ");
				string EntradaDescricao = Console.ReadLine();

				if(EntradaDescricao.Trim() != "")
					return EntradaDescricao;
				
				Console.WriteLine();
				Console.WriteLine("Descrição invalida!");
				Console.WriteLine();

			}
		}
    }
}
