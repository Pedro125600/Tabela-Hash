namespace att5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("De o tamanho da tabela:");
            int tm = int.Parse(Console.ReadLine());
            HashHibrida rehash = new HashHibrida(tm);
            bool cont = true;

            int[] valores = { 10, 20, 30, 40, 50, 60 };
            for (int i = 0; i < valores.Length; i++)
            {
                rehash.Inserir(valores[i]);
            }


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
}

    class HashHibrida
    {
        private int tamTabela;
        private ListaDupla[] tabela; 
        private int[] reserva;
        private int tamReserva;
        private int numReserva;

        public HashHibrida(int tamTabela)
        {
            this.tamTabela = tamTabela;
            this.tamReserva = 5;
            tabela = new ListaDupla[tamTabela];
            reserva = new int[tamReserva];
            numReserva = 0;

            for (int i = 0; i < tamTabela; i++)
            {
                tabela[i] = new ListaDupla();
            }

            for (int i = 0; i < tamReserva; i++)
            {
                reserva[i] = 0;
            }
        }

        private int Hash(int x)
        {
            return x % tamTabela;
        }

        public void Inserir(int x)
        {
            int indice = Hash(x);
            ListaDupla lista = tabela[indice];

            if (lista.Tamanho() < 3)
            {
                lista.inserirFim(x);
            }
            else if (numReserva < tamReserva)
            {
                reserva[numReserva++] = x;
            }
            else
            {
                throw new Exception("Erro! Sem espaço na área de overflow.");
            }
        }

        public bool Pesquisar(int x)
        {
            int indice = Hash(x);
            ListaDupla lista = tabela[indice];

            if (lista.Buscar(ref x))
            {
                return true;
            }

            for (int i = 0; i < numReserva; i++)
            {
                if (reserva[i] == x)
                {
                    return true;
                }
            }

            return false;
        }

    public int Remover(int x)
    {
        int indice = Hash(x);
        ListaDupla lista = tabela[indice];

        int resp = -1;

        for (int i = 0; i < lista.Tamanho(); i++)
        {
            if (lista.Buscar(ref x))
            {
                resp = lista.remover(i);
            }
        }


        for (int i = 0; i < numReserva; i++)
        {
            if (reserva[i] == x)
            {
                int removido = reserva[i];


                for (int j = i; j < numReserva - 1; j++)
                {
                    reserva[j] = reserva[j + 1];
                }

                reserva[--numReserva] = 0;
                resp = removido;
            }
        }
        return resp;
    }

    public void Mostrar()
        {
            Console.WriteLine("Tabela Hash:");
            for (int i = 0; i < tamTabela; i++)
            {
                Console.Write($"{i}: ");
                tabela[i].Mostrar();
            }

            Console.WriteLine("Área de Overflow:");
            for (int i = 0; i < tamReserva; i++)
            {
                if (reserva[i] != 0)
                {
                    Console.Write($"{reserva[i]} ");
                }
            }
            Console.WriteLine();
        }
    }

    class CelulaDupla
    {
        private int elemento;
        private CelulaDupla prox, ant;
        public CelulaDupla(int elemento)
        {
            this.elemento = elemento;
            this.prox = null;
            this.ant = null;
        }
        public CelulaDupla()
        {
            this.elemento = 0;
            this.prox = null;
            this.ant = null;
        }
        public CelulaDupla Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public CelulaDupla Ant
        {
            get { return ant; }
            set { ant = value; }
        }
        public int Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }

    class ListaDupla
    {
        private CelulaDupla primeiro, ultimo;
        public ListaDupla()
        {
            primeiro = new CelulaDupla();
            ultimo = primeiro;
        }

        public void Ordenar()
        {
            if (primeiro == ultimo || primeiro.Prox == null)
                return;

            bool trocou;
            do
            {
                trocou = false;
                CelulaDupla atual = primeiro.Prox;

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

        public void InserirOrdenar(int x)
        {
            if (primeiro == ultimo)
                inserirFim(x);
            else
            {
                int cont = 0;
                for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox, cont++)
                {
                    if (i.Elemento > x)
                    {
                        inserir(x, cont);
                        return;
                    }
                }

            }

        }
        public int Tamanho()
        {

            int cont = 0;

            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox) { cont++; };
            return cont;

        }

        public void inserirInicio(int x)
        {
            CelulaDupla tmp = new CelulaDupla(x);
            tmp.Ant = primeiro;
            tmp.Prox = primeiro.Prox;
            primeiro.Prox = tmp;
            if (primeiro == ultimo)
            {
                ultimo = tmp;
            }
            else
            {
                tmp.Prox.Ant = tmp;
            }
            tmp = null;
        }

        public void inserirFim(int x)
        {
            ultimo.Prox = new CelulaDupla(x);
            ultimo.Prox.Ant = ultimo;
            ultimo = ultimo.Prox;
        }

        public int removerInicio()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            CelulaDupla tmp = primeiro;
            primeiro = primeiro.Prox;
            int elemento = primeiro.Elemento;
            tmp.Prox = primeiro.Ant = null;
            tmp = null;
            return elemento;
        }

        public int removerFim()
        {
            if (primeiro == ultimo)
                throw new Exception("Erro!");
            int elemento = ultimo.Elemento;
            ultimo = ultimo.Ant;
            ultimo.Prox.Ant = null;
            ultimo.Prox = null;
            return elemento;
        }

        public void inserir(int x, int pos)
        {
            int tamanho = Tamanho();
            if (pos < 0 || pos > tamanho)
            {
                throw new Exception("Erro!");
            }
            else if (pos == 0)
            {
                inserirInicio(x);
            }
            else if (pos == tamanho)
            {
                inserirFim(x);
            }
            else
            {
                CelulaDupla i = primeiro;
                for (int j = 0; j < pos; j++, i = i.Prox) ;
                CelulaDupla tmp = new CelulaDupla(x);
                tmp.Ant = i;
                tmp.Prox = i.Prox;
                tmp.Ant.Prox = tmp.Prox.Ant = tmp;
                tmp = i = null;

            }
        }

        public int remover(int pos)
        {
            int elemento, tamanho = Tamanho();
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

        public void Inverter()
        {

            ListaDupla temp = new ListaDupla();


            while (primeiro.Prox != null)
            {
                temp.inserirFim(removerFim());
            }

            while (temp.primeiro.Prox != null)
            {
                inserirFim(temp.removerInicio());
            }

        }

        public bool Buscar(ref int x)
        {

            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox)
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
            for (CelulaDupla i = primeiro.Prox; i != null; i = i.Prox)
            {
                Console.Write(i.Elemento + " ");
            }
            Console.WriteLine("]");
        }
    }


