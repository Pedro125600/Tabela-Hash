using System.Reflection.Emit;

namespace att3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("De o tamanho da tabela:");
            int tm = int.Parse(Console.ReadLine());
            Hash rehash = new Hash(tm);
            bool cont = true;

            while (cont == true)
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
                        Console.WriteLine("Digite o valor a ser inserido:");
                        int inserirValor = int.Parse(Console.ReadLine());

                        rehash.Inserir(inserirValor);
                        Console.WriteLine("Valor inserido com sucesso!");

                        break;

                    case 2:
                        Console.WriteLine("Digite o valor a ser pesquisado:");
                        int pesquisarValor = int.Parse(Console.ReadLine());

                        if (rehash.Pesquisar(pesquisarValor))
                            if (rehash.Pesquisar(pesquisarValor))
                            {
                                Console.WriteLine("Valor encontrado!");
                            }
                            else
                            {
                                Console.WriteLine("Valor não encontrado!");
                            }
                        break;

                    case 3:
                        Console.WriteLine("Digite o valor a ser removido:");
                        int removerValor = int.Parse(Console.ReadLine());
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

        public Lista[] tabela;

        private int tamTabela;
        public int TamTabela { get { return tamTabela; } set { tamTabela = value; } }

        public Hash(int m)
        {
            tamTabela = m;
            tabela = new Lista[m];
            for (int i = 0; i < m; i++)
            {
                tabela[i] = new Lista();
            }
        }

        private int HashTrasformar(int x)
        {
            return x % tamTabela;
        }

        public void Inserir(int x)
        {
            if (x == 0)
                throw new Exception("Erro! Valor não pode ser zero.");

            int indice = HashTrasformar(x);
            tabela[indice].InserirFim(x);
        }

        public bool Pesquisar(int x)
        {
            int indice = HashTrasformar(x);
            return tabela[indice].Buscar(ref x);
        }

        public void Mostrar()
        {
            for (int i = 0; i < tamTabela; i++)
            {
                Console.Write($"{i}: ");
                tabela[i].Mostrar();
            }
        }


        public int Remover(int x)
        {
            int indice = HashTrasformar(x);

            Lista listaNoIndice = tabela[indice];
            int posicao = -1;
            Celula atual = listaNoIndice.Primeiro.Prox;
            int pos = 0;
            while (atual != null)
            {
                if (atual.Elemento == x)
                {
                    posicao = pos;
                    break;
                }
                atual = atual.Prox;
                pos++;
            }

            if (posicao >= 0)
            {
              return listaNoIndice.Remover(posicao);
            }
            else
            {
                return -1;
            }
        }

    }



    class Celula
    {
        private int elemento;
        private Celula prox;
        public Celula(int elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public Celula()
        {
            this.elemento = 0;
            this.prox = null;
        }
        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public int Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }


    class Lista
    {
        private Celula primeiro, ultimo;
        public Lista()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }

        public Celula Primeiro { get { return primeiro; } set { value = primeiro; } }

        public void InserirInicio(int x)
        {
            Celula tmp = new Celula(x);
            tmp.Prox = primeiro.Prox;
            primeiro.Prox = tmp;
            if (primeiro == ultimo)
            {
                ultimo = tmp;
            }
            tmp = null;
        }

        public int RemoverFim()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            Celula i;
            for (i = primeiro; i.Prox != ultimo; i = i.Prox) ;
            int elemento = ultimo.Elemento;
            ultimo = i;
            i = ultimo.Prox = null;
            return elemento;
        }

        public void InserirOrdenar(int x)
        {
            if (primeiro == ultimo)
                InserirFim(x);
            else
            {
                int cont = 0;
                for (Celula i = primeiro.Prox; i != null; i = i.Prox, cont++)
                {
                    if (i.Elemento > x)
                    {
                        Inserir(x, cont);
                        return;
                    }
                }

            }

        }


        public void InserirFim(int x)
        {
            ultimo.Prox = new Celula(x);
            ultimo = ultimo.Prox;
        }



        public void Inserir(int x, int pos)
        {
            int tamanho = Tamanho();
            if (pos < 0 || pos > tamanho)
            {
                throw new Exception("Erro!");
            }
            else if (pos == 0)
            {
                InserirInicio(x);
            }
            else if (pos == tamanho)
            {
                InserirFim(x);
            }
            else
            {
                Celula i = primeiro;
                for (int j = 0; j < pos; j++, i = i.Prox) ;
                Celula tmp = new Celula(x);
                tmp.Prox = i.Prox;
                i.Prox = tmp;
                tmp = i = null;
            }
        }

        public int RemoverInicio()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            Celula tmp = primeiro;
            primeiro = primeiro.Prox;
            int elemento = primeiro.Elemento;
            tmp.Prox = null;
            tmp = null;
            return elemento;
        }

        public int Remover(int pos)
        {
            int elemento, tamanho = Tamanho();
            if (primeiro == ultimo || pos < 0 || pos >= tamanho)
            {
                throw new Exception("Erro!");
            }
            else if (pos == 0)
            {
                elemento = RemoverInicio();
            }
            else if (pos == tamanho - 1)
            {
                elemento = RemoverFim();
            }
            else
            {
                Celula i = primeiro;
                for (int j = 0; j < pos; j++, i = i.Prox) ;
                Celula tmp = i.Prox;
                elemento = tmp.Elemento; i.Prox = tmp.Prox;
                tmp.Prox = null; i = tmp = null;
            }
            return elemento;
        }

        public void Inverter()
        {
            Lista temp = new Lista();



            while (primeiro.Prox != null)
            {
                temp.InserirFim(RemoverFim());
            }

            while (temp.primeiro.Prox != null)
            {
                InserirFim(temp.RemoverInicio());
            }
        }

        public int Tamanho()
        {

            int cont = 0;

            for (Celula i = primeiro.Prox; i != null; i = i.Prox) { cont++; }
            return cont;

        }

        public bool Buscar(ref int x)
        {

            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
            {
                if (i.Elemento == x)
                {
                    x = i.Elemento;
                    return true;

                }

            }

            return false;
        }
        public void Mostrar()
        {
            Console.Write("[");
            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
            {
                Console.Write(i.Elemento + " ");
            }
            Console.WriteLine("]");
        }

        public void Ordenar()
        {
            if (primeiro == ultimo || primeiro.Prox == null)
                return;

            bool trocou;
            do
            {
                trocou = false;
                Celula atual = primeiro.Prox;

                while (atual != null && atual.Prox != null)
                {
                    if (atual.Elemento > atual.Prox.Elemento)
                    {
                        int temp = atual.Elemento;
                        atual.Elemento = atual.Prox.Elemento;
                        atual.Prox.Elemento = temp;

                        trocou = true;
                    }
                    atual = atual.Prox;
                }
            } while (trocou);

        }

   
    }



}