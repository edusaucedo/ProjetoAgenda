using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    private static string caminhoArquivoContatos = "contatos.txt";
    private static List<Contato> contatos = new List<Contato>();
    private static Regex regexTelefone = new Regex(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$");

    static void Main(string[] args)
    {
        CarregarContatos();

        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("AGENDA TELEFÔNICA");
            Console.WriteLine("1. Adicionar Contato");
            Console.WriteLine("2. Remover Contato");
            Console.WriteLine("3. Mostrar Contatos");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");

            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    AdicionarContato();
                    break;
                case "2":
                    RemoverContato();
                    break;
                case "3":
                    ExibirContatos();
                    break;
                case "4":
                    Salvar();
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    public static void CarregarContatos()
    {
        contatos.Clear();
        if (File.Exists(caminhoArquivoContatos))
        {
            using (var sr = new StreamReader(caminhoArquivoContatos))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string nome = linha;
                    string numeroTelefone = sr.ReadLine();
                    contatos.Add(new Contato(nome, numeroTelefone));
                }
            }
        }
    }

    public static void Salvar()
    {
        using (var sw = new StreamWriter(caminhoArquivoContatos))
        {
            foreach (var contato in contatos)
            {
                sw.WriteLine(contato.Nome);
                sw.WriteLine(contato.NumeroTelefone);
            }
        }
    }

    public static void AdicionarContato()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Telefone: ");
        string numeroTelefone = Console.ReadLine();

        if (regexTelefone.IsMatch(numeroTelefone))
        {
            contatos.Add(new Contato(nome, numeroTelefone));
            Console.WriteLine("Contato adicionado");
        }
        else
        {
            Console.WriteLine("O Numero do telefone é inválido");
        }

        Console.ReadKey();
    }

    public static void RemoverContato()
    {
        if (contatos.Count == 0)
        {
            Console.WriteLine("Nenhum contato adicionado.");
            Console.ReadKey();
            return;
        }

        ExibirContatos();

        Console.Write("Digite o número do contato que deseja remover: ");
        int indice;
        if (int.TryParse(Console.ReadLine(), out indice) && indice >= 1 && indice <= contatos.Count)
        {
            contatos.RemoveAt(indice - 1);
            Console.WriteLine("Contato removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Número de contato inválido.");
        }

        Console.ReadKey();
    }

    public static void ExibirContatos()
    {
        Console.Clear();
        Console.WriteLine("LISTA DE CONTATOS");
        Console.WriteLine("-----------------------------------");
        if (contatos.Count == 0)
        {
            Console.WriteLine("Nenhum contato cadastrado.");
        }
        else
        {
            for (int i = 0; i < contatos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {contatos[i].Nome} - {contatos[i].NumeroTelefone}");
            }
        }
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Pressione qualquer tecla para voltar.");
        Console.ReadKey();
    }

    class Contato
    {
        public Contato(string nome, string numeroTelefone)
        {
            Nome = nome;
            NumeroTelefone = numeroTelefone;
        }
        public string Nome { get; set; }
        public string NumeroTelefone { get; set; }
    }
} 
