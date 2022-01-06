using System;

namespace dio_series_app
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = "";

            while (opcaoUsuario != "X")
            {
                opcaoUsuario = ObterOpcaoUsuario().ToUpper();
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
                        ExcluiSerie();
                        break;
                    case "5":
                        DetalhesSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    case "X":
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar series");
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma serie cadastrada");
                return;
            }

            foreach (var serie in lista)
            {
                Console.WriteLine($"#ID {serie.retornaId()}: - {serie.retornaTitulo()} {(serie.retornaExcluido() ? "Excluido" : "")}");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");
            Serie novaSerie = ColetarDadosSerie(repositorio.ProximoId());

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            try
            {
                Console.WriteLine("Digite o id da serie: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                checarSerie(indiceSerie);
                Serie atualizaSerie = ColetarDadosSerie(indiceSerie);

                repositorio.Atualiza(indiceSerie, atualizaSerie);

                Console.WriteLine("Serie atualizada com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private static void ExcluiSerie()
        {
            try
            {
                Console.WriteLine("Digite o id da serie: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                checarSerie(indiceSerie);

                repositorio.Exclui(indiceSerie);

                Console.WriteLine("Serie excluida com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

        private static void DetalhesSerie()
        {
            try
            {
                Console.WriteLine("Digite o id da serie: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                Serie serie = checarSerie(indiceSerie);

                Console.WriteLine(serie.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Series App a seu dispor!!!");
            Console.WriteLine("Informe a opcao desejada:");

            Console.WriteLine("1 - Listar series");
            Console.WriteLine("2 - Inserir nova serie");
            Console.WriteLine("3 - Atualizar serie");
            Console.WriteLine("4 - Excluir serie");
            Console.WriteLine("5 - Visualizar detalhes da serie");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static Serie ColetarDadosSerie(int id)
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("Digite o genero entre as opcoes acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o titulo da serie: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano de inicio da serie: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descricao da serie: ");
            string entradaDescricao = Console.ReadLine();

            Serie serie = new Serie(
                id: id,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            return serie;
        }

        private static Serie checarSerie(int id)
        {
            if (id > repositorio.ProximoId() ||
                repositorio.RetornaPorId(id).Excluido == true)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(id), message: "Id informado nao existe");
            }
            return repositorio.RetornaPorId(id);
        }
    }
}