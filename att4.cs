using System;

namespace att4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o tamanho da tabela:");
            int tm = int.Parse(Console.ReadLine());
            Hash rehash = new Hash(tm);
            bool cont = true;

            string[] palavras = { "casa", "saco", "as", "saca", "caso" };
            for (int i = 0; i < palavras.Length; i++)
            {
                rehash.Inserir(palavras[i]);
            }


            rehash.Mostrar();

            Console.ReadLine();
            while (cont)
            {
                Console.Clear();
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Inserir");
                Console.WriteLine("2 - Pesquisar");
                Console.WriteLine("3 - Remover");
                Console.WriteLine("4 - Sair");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Digite uma palavra a ser inserida:");
                        string inserirString = Console.ReadLine();
                        rehash.Inserir(inserirString);
                        Console.WriteLine("inserida com sucesso!");
                        break;

                    case 2:
                        Console.WriteLine("Digite a palavra a ser pesquisada:");
                        string pesquisarString = Console.ReadLine();

                        if (rehash.Pesquisar(pesquisarString))
                        {
                            Console.WriteLine("encontrada!");
                        }
                        else
                        {
                            Console.WriteLine("não encontrada!");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Digite a palavra a ser removido:");
                        string removerValor = Console.ReadLine();
                        int valorRemovido = rehash.Remover(removerValor);
                        if (valorRemovido != -1)
                        {
                            Console.WriteLine($"Valor {valorRemovido} removido com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Valor não encontrado para remoção!");
                        }
                        break;

                    case 4:
                        cont = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                rehash.Mostrar();
                Console.ReadLine();
            }
        }
    }

    class Hash
    {
        public ListaDupla[] tabela;
        private int tamTabela;

        public Hash(int m)
        {
            tamTabela = m;
            tabela = new ListaDupla[m];
            for (int i = 0; i < m; i++)
            {
                tabela[i] = new ListaDupla();
            }
        }

        private int HashTrasformar(string s)
        {
            int somaASCII = 0;
            foreach (char c in s)
            {
                somaASCII += (int)c;
            }
            return somaASCII % tamTabela;
        }

        public void Inserir(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new Exception("Erro! String não pode ser vazia ou nula.");

            int indice = HashTrasformar(s);
            tabela[indice].inserirFim(s);
        }

        public bool Pesquisar(string s)
        {
            int indice = HashTrasformar(s);
            return tabela[indice].Buscar(ref s);
        }

        public void Mostrar()
        {
            for (int i = 0; i < tamTabela; i++)
            {
                Console.Write($"{i}: ");
                tabela[i].Mostrar();
            }
        }

        public int Remover(string s)
        {
            int indice = HashTrasformar(s); 
            CelulaDupla atual = tabela[indice].Primeiro.Prox; 

            int posicao = 0;

            while (atual != null)
            {
                if (atual.Elemento == s)
                {
                    tabela[indice].remover(posicao);
                    return posicao;
                }
                atual = atual.Prox;
                posicao++;
            }

            return -1;
        }
    }

    class CelulaDupla
    {
        private string elemento;
        private CelulaDupla prox, ant;

        public CelulaDupla(string elemento)
        {
            this.elemento = elemento;
            this.prox = null;
            this.ant = null;
        }

        public CelulaDupla()
        {
            this.elemento = null;
            this.prox = null;
            this.ant = null;
        }

        public CelulaDupla Prox { get => prox; set => prox = value; }
        public CelulaDupla Ant { get => ant; set => ant = value; }
        public string Elemento { get => elemento; set => elemento = value; }
    }

    class ListaDupla
    {
        private CelulaDupla primeiro, ultimo;

        public int Tamanho()
        {

            int cont = 0;

            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox) { cont++; };
            return cont;

        }

        public string removerInicio()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            CelulaDupla tmp = primeiro;
            primeiro = primeiro.Prox;
            string elemento = primeiro.Elemento;
            tmp.Prox = primeiro.Ant = null;
            tmp = null;
            return elemento;
        }

        public string removerFim()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            string elemento = ultimo.Elemento;
            ultimo = ultimo.Ant;
            ultimo.Prox.Ant = null;
            ultimo.Prox = null;
            return elemento;
        }


        public ListaDupla()
        {
            primeiro = new CelulaDupla();
            ultimo = primeiro;
        }

        public CelulaDupla Primeiro { get { return primeiro; } set { primeiro = value; } }

        public void inserirFim(string s)
        {
            ultimo.Prox = new CelulaDupla(s);
            ultimo.Prox.Ant = ultimo;
            ultimo = ultimo.Prox;
        }

        public  string remover(int pos)
        {
            string elemento;
            int tamanho = Tamanho();
            if (primeiro == ultimo)
            {
                throw new Exception("Erro!");
            }
            else if (pos < 0 || pos >= tamanho)
            {
                throw new Exception("Erro!");
            }
            else if (pos == 0)
            {
                elemento = removerInicio();
            }
            else if (pos == tamanho - 1)
            {
                elemento = removerFim();
            }
            else
            {
                CelulaDupla i = primeiro.Prox;
                for (int j = 0; j < pos; j++, i = i.Prox) ;
                i.Ant.Prox = i.Prox;
                i.Prox.Ant = i.Ant;
                elemento = i.Elemento;
                i.Prox = i.Ant = null;
                i = null;
            }
            return elemento;
        }


        public bool Buscar(ref string s)
        {
            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox)
            {
                if (i.Elemento == s)
                {
                    s = i.Elemento;
                    return true;
                }
            }
            return false;
        }

        public void Mostrar()
        {
            Console.Write("[");
            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox)
            {
                Console.Write(i.Elemento + " ");
            }
            Console.WriteLine("]");
        }
    }
}
