using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Grupo: Eduardo Paes, Giovanna de Melo, Mariana Alves e Nicole Assis

namespace APS
{
    public class Paciente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public double Pressao { get; set; }
        public double Temperatura { get; set; }
        public double Oxigenacao { get; set; }
        public string Prioridade { get; private set; }

        public Paciente(string cpf, string nome, double pressao, double temperatura, double oxigenacao)
        {
            CPF = cpf.Trim();
            Nome = nome;
            Pressao = pressao;
            Temperatura = temperatura;
            Oxigenacao = oxigenacao;
            AtualizarPrioridade();
        }

        public void Atualizar(double pressao, double temperatura, double oxigenacao)
        {
            Pressao = pressao;
            Temperatura = temperatura;
            Oxigenacao = oxigenacao;
            AtualizarPrioridade();
        }

        public void AtualizarPrioridade()
        {
            if (Pressao > 18 || Temperatura > 39 || Oxigenacao < 90)
                Prioridade = "Vermelha";
            else if (Pressao > 14 || Temperatura > 37.5 || Oxigenacao < 95)
                Prioridade = "Amarela";
            else
                Prioridade = "Verde";
        }

        public void ImprimirInfo()
        {
            switch (Prioridade)
            {
                case "Vermelha":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Amarela":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "Verde":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine($"{CPF} | {Nome} | PA: {Pressao} | Temp: {Temperatura}°C | O2: {Oxigenacao}% | Prioridade: {Prioridade}");
            Console.ResetColor();
        }

        public override string ToString()
        {
            return $"{CPF} | {Nome} | PA: {Pressao} | Temp: {Temperatura}°C | O2: {Oxigenacao}% | Prioridade: {Prioridade}";
        }
    }

    public class TabelaHash<TKey, TValue>
    {
        private readonly int capacidade;
        private readonly LinkedList<KeyValuePair<TKey, TValue>>[] buckets;

        public TabelaHash(int capacidade = 10)
        {
            this.capacidade = capacidade;
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacidade];
            for (int i = 0; i < capacidade; i++)
                buckets[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        private int GetIndice(TKey chave)
        {
            return Math.Abs(chave.GetHashCode()) % capacidade;
        }

        public void Inserir(TKey chave, TValue valor)
        {
            int indice = GetIndice(chave);
            var bucket = buckets[indice];

            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(chave))
                {
                    bucket.Remove(node);
                    break;
                }
                node = node.Next;
            }
            bucket.AddLast(new KeyValuePair<TKey, TValue>(chave, valor));
        }

        public bool Remover(TKey chave)
        {
            int indice = GetIndice(chave);
            var bucket = buckets[indice];
            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(chave))
                {
                    bucket.Remove(node);
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public bool Buscar(TKey chave, out TValue valor)
        {
            int indice = GetIndice(chave);
            var bucket = buckets[indice];
            foreach (var par in bucket)
            {
                if (par.Key.Equals(chave))
                {
                    valor = par.Value;
                    return true;
                }
            }
            valor = default;
            return false;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> Todos()
        {
            foreach (var bucket in buckets)
                foreach (var par in bucket)
                    yield return par;
        }

        public void ExibirTabela()
        {
            for (int i = 0; i < capacidade; i++)
            {
                Console.WriteLine($"Bucket {i}:");
                foreach (var par in buckets[i])
                {
                    if (par.Value is Paciente p)
                        p.ImprimirInfo();
                    else
                        Console.WriteLine($"{par.Key} => {par.Value}");
                }
                if (buckets[i].Count == 0)
                    Console.WriteLine("  (vazio)");
            }
        }
    }

    public class SistemaClinico
    {
        private TabelaHash<string, Paciente> tabelaPacientes;
        private Queue<Paciente> filaTriagem;
        private Stack<Paciente> historico;

        public SistemaClinico(int capacidadeHash)
        {
            tabelaPacientes = new TabelaHash<string, Paciente>(capacidadeHash);
            filaTriagem = new Queue<Paciente>();
            historico = new Stack<Paciente>();
        }

        public void CadastrarPaciente()
        {
            Console.Write("CPF: ");
            string cpf = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                Console.WriteLine("CPF não pode ser vazio.");
                return;
            }
            if (tabelaPacientes.Buscar(cpf, out _))
            {
                Console.WriteLine("Já existe paciente com esse CPF.");
                return;
            }

            Console.Write("Nome completo: ");
            string nome = Console.ReadLine();
            Console.Write("Pressão arterial: ");
            double pressao = LerDouble();
            Console.Write("Temperatura corporal (°C): ");
            double temperatura = LerDouble();
            Console.Write("Nível de oxigenação (%): ");
            double oxigenacao = LerDouble();

            var paciente = new Paciente(cpf, nome, pressao, temperatura, oxigenacao);
            tabelaPacientes.Inserir(cpf, paciente);
            filaTriagem.Enqueue(paciente);

            Console.WriteLine("Paciente cadastrado e adicionado à fila de triagem.");
        }

        public void BuscarPaciente()
        {
            Console.Write("Digite o CPF para busca: ");
            string cpf = Console.ReadLine().Trim();
            if (tabelaPacientes.Buscar(cpf, out Paciente paciente))
            {
                paciente.ImprimirInfo();
            }
            else
            {
                Console.WriteLine("Paciente não encontrado.");
            }
        }

        public void AtualizarPaciente()
        {
            Console.Write("Digite o CPF para atualizar: ");
            string cpf = Console.ReadLine().Trim();
            if (tabelaPacientes.Buscar(cpf, out Paciente paciente))
            {
                Console.Write("Nova Pressão arterial: ");
                double pressao = LerDouble();
                Console.Write("Nova Temperatura corporal (°C): ");
                double temperatura = LerDouble();
                Console.Write("Novo Nível de oxigenação (%): ");
                double oxigenacao = LerDouble();

                paciente.Atualizar(pressao, temperatura, oxigenacao);
                tabelaPacientes.Inserir(cpf, paciente); // Garante atualização na hash
                Console.WriteLine("Paciente atualizado.");
            }
            else
            {
                Console.WriteLine("Paciente não encontrado.");
            }
        }

        public void RemoverPaciente()
        {
            Console.Write("Digite o CPF para remoção: ");
            string cpf = Console.ReadLine().Trim();
            if (tabelaPacientes.Remover(cpf))
            {
                Console.WriteLine("Paciente removido.");
            }
            else
            {
                Console.WriteLine("Paciente não encontrado.");
            }
        }

        public void ExibirTabela()
        {
            tabelaPacientes.ExibirTabela();
        }

        public void RealizarTriagemEAtendimento()
        {
            Console.WriteLine("\n--- Triagem e Atendimento ---");
            var vermelha = new Queue<Paciente>();
            var amarela = new Queue<Paciente>();
            var verde = new Queue<Paciente>();

            while (filaTriagem.Count > 0)
            {
                var paciente = filaTriagem.Dequeue();
                paciente.ImprimirInfo();
                switch (paciente.Prioridade)
                {
                    case "Vermelha":
                        vermelha.Enqueue(paciente);
                        break;
                    case "Amarela":
                        amarela.Enqueue(paciente);
                        break;
                    default:
                        verde.Enqueue(paciente);
                        break;
                }
            }

            AtenderFila(vermelha);
            AtenderFila(amarela);
            AtenderFila(verde);
        }

        private void AtenderFila(Queue<Paciente> fila)
        {
            while (fila.Count > 0)
            {
                var paciente = fila.Dequeue();
                Console.Write("Atendendo: ");
                paciente.ImprimirInfo();
                historico.Push(paciente);
            }
        }

        public void MostrarHistorico()
        {
            Console.WriteLine("\n--- Histórico de Atendimentos ---");
            if (historico.Count == 0)
            {
                Console.WriteLine("Nenhum atendimento realizado.");
                return;
            }
            foreach (var paciente in historico)
            {
                Console.WriteLine($"{paciente.Nome} ({paciente.CPF}) - Prioridade: {paciente.Prioridade}");
            }
        }

        private double LerDouble()
        {
            double valor;
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Entrada inválida. Digite um número: ");
            }
            return valor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int capacidadeHash = 10;
            SistemaClinico sistema = new SistemaClinico(capacidadeHash);

            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1. Cadastrar paciente");
                Console.WriteLine("2. Buscar paciente por CPF");
                Console.WriteLine("3. Atualizar dados clínicos de paciente");
                Console.WriteLine("4. Remover paciente");
                Console.WriteLine("5. Exibir tabela hash");
                Console.WriteLine("6. Realizar triagem e atendimento");
                Console.WriteLine("7. Exibir histórico de atendimentos");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1": sistema.CadastrarPaciente(); break;
                    case "2": sistema.BuscarPaciente(); break;
                    case "3": sistema.AtualizarPaciente(); break;
                    case "4": sistema.RemoverPaciente(); break;
                    case "5": sistema.ExibirTabela(); break;
                    case "6": sistema.RealizarTriagemEAtendimento(); break;
                    case "7": sistema.MostrarHistorico(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }
    }
}
