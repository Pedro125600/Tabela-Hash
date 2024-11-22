using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("De o tamanho da tabela:");
            int tm = int.Parse(Console.ReadLine());
            Overflow overflow = new Overflow(tm);
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

                        overflow.Inserir(inserirValor);
                        Console.WriteLine("Valor inserido com sucesso!");

                        break;

                    case 2:
                        Console.WriteLine("Digite o valor a ser pesquisado:");
                        int pesquisarValor = int.Parse(Console.ReadLine());

                        if (overflow.Pesquisar(pesquisarValor))
                        if (overflow.Pesquisar(pesquisarValor))
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
                        int valorRemovido = overflow.Remover(removerValor);
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

                overflow.Mostrar();
                Console.ReadLine();
            }
        }
    }
}



class Overflow
{
    private int[] array;
    private int tamTabela;
    private int tamReserva;
    private int numReserva;

    public Overflow(int tm)
    {
        tamReserva = 3;
        array = new int[tm + tamReserva];
        tamTabela = tm;
        numReserva = 0;
    }

    public int[] Array { get { return array; } set { array = value; } }
    public int TamTabela { get { return tamTabela; } set { tamTabela = value; } }

    public int TamReserva { get { return tamReserva; } set { tamReserva = value; } }


    public int Hash(int x)
    {
        return x % tamTabela;
    }




    public void Inserir(int x)
    {
        int i = Hash(x);
        if (x == 0)
        {
            throw new Exception("Erro!");
        }
        else if (Array[i] == 0)
        {
            Array[i] = x;
        }
        else if (numReserva < tamReserva)
        {
            Array[tamTabela + numReserva] = x;
            numReserva++;
        }
        else
        {
            throw new Exception("Erro!");
        }
    }

    public bool Pesquisar(int x)
    {
        int i = Hash(x);

        if (x == array[i])
        {
            return true;
        }
        else
        {
            for (int j = 0; j < numReserva; j++)
            {
                if (x == array[tamTabela + numReserva])
                    return true;
            }
        }

        return false;
    }

    public int Remover(int x)
    {
        int i = Hash(x);
        int resp = -1;

        if (x == array[i])
        {
            resp = array[i];
            array[i] = -1;

        }
        else
        {
            for (int j = 0; j < numReserva; j++)
            {
                if (x == array[tamTabela + numReserva])
                    resp = array[tamTabela + numReserva];
                array[tamTabela + numReserva] = -1;
            }
        }

        return resp;
    }

    public void Mostrar()
    {
        for (int i = 0; i < tamReserva + tamTabela; i++)
        {
            Console.Write(array[i] + " ");
        }
    }


}

