using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace FracoesEmpilhadas
{
    public class Fracoes
    {
        /// <summary>
        /// Divisor (o de baixo)
        /// </summary>
        public long lngDivisor;

        /// <summary>
        /// Dividendo (o de cima)
        /// </summary>
        public long lngDividendo;

        /// <summary>
        /// Coonstrutor com dois párâmetros (constrói pronto)
        /// </summary>
        /// <param name="pDividendo">(O de cima)</param>
        /// <param name="pDivisor">(O de baixo)</param>
        /// <exception cref="System.DivideByZeroException"></exception>
        public Fracoes(long pDividendo, long pDivisor)
        {
            if (pDivisor == 0) //Não pode ter frações com divisor zero
            {
                throw new System.DivideByZeroException();
            }
            this.lngDivisor = pDivisor;
            this.lngDividendo = pDividendo;
        }

        /// <summary>
        /// COnstrutor sem parâmetros. Cria 1/1
        /// </summary>
        public Fracoes()
        {
            lngDivisor = 1;
            lngDividendo = 1;
        }

        /// <summary>
        /// Simplifica o objeto atual da fração
        /// </summary>
        public void Simplificador()
        {
            if (lngDividendo == 0 || lngDivisor == 0) // tratamento de divisao com trermo 0
            {
                return;
            }
            long lngNumOrg1 = lngDividendo;
            long lngNumOrg2 = lngDivisor;
            long lngRecebeVal = 0;
            //Ninguém negativo
            if (lngDividendo < 0)
            {
                lngDividendo = -lngDividendo;
            }
            if (lngDivisor < 0)
            {
                lngDivisor = -lngDivisor;
            }
            //Se forem iguais, 1/1
            /*  if (lngDividendo == lngDivisor)// condiçao caso dividendo e divisor tenham mesmo valor
              {
                  lngDividendo = 1;
                  lngDivisor = 1;
              }*/
            long lngOMenor = lngDivisor;
            if (lngDividendo < lngDivisor)
            {
                lngOMenor = lngDividendo;
            }
            //Apenas uma pesquisa. Sem vetor
            for (long lngI = lngOMenor; lngI > 0; lngI--)
            {
                if (lngDividendo % lngI == 0 && lngDivisor % lngI == 0)
                {

                        lngRecebeVal = lngI;
                        break;
                }
            }
            /*long[] lngCadeia = new long[lngDivisor + 1];
            for (long i = 1; i < lngDivisor + 1; i++)
            {
                if (lngDividendo % i == 0 && lngDivisor % i == 0)
                {
                    lngCadeia[i] = i;
                }
            }
            for (long a = 1; a < lngDivisor + 1; a++)
            {
                if (lngCadeia[a] > lngCadeia[a - 1])
                {
                    lngRecebeVal = lngCadeia[a];
                }
            }*/
            lngDividendo = (lngNumOrg1 / lngRecebeVal);
            lngDivisor = (lngNumOrg2 / lngRecebeVal);
        }//fim da funçao

        /// <summary>
        /// metodo de retorno para converter em fraçao qualquer numero recebido;
        /// </summary>
        /// <param name="pConversor"> parametro da string em cadeia principal que será fragmentado e analizado</param>
        /// <returns>True se for uma fração válida. False se não for uma fração válida</returns>
        public bool TransformaEmFracao(string pConversor)
        {
            if (pConversor == "/" || pConversor == "+" || pConversor == "-" || pConversor == "*") //Só pra poder usar a barra na divisão
            {
                return false;
            }
            foreach (char chrC in pConversor) //Não deixa colocar nenhuma letra
            {
                if (Char.IsLetter(chrC))
                {
                    return false;
                }
            }
            string[] strQuebraValor = pConversor.Split('/');
            if (strQuebraValor.Length == 2) //Veio uma fração pronta
            {
                if (long.TryParse(strQuebraValor[0], out long lngDvdo)) //Valida se a primeira parte é um long
                {
                    lngDividendo = lngDvdo;
                }
                else
                {
                    return false;
                }
                if (long.TryParse(strQuebraValor[1], out long lngDvsr)) //Valida se a segunda parte é um long
                {
                    if (lngDvsr != 0)
                    {
                        lngDivisor = lngDvsr;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (long.TryParse(pConversor, out long lngRecebeTrue))
            {
                lngDividendo = lngRecebeTrue;
                lngDivisor = 1;
            }
            else if (decimal.TryParse(pConversor, out decimal decNrecebeTrue))
            {
                decimal decTentaConverte = decNrecebeTrue;
                int intI = 1;
                do
                {
                    decTentaConverte *= 10;
                    intI *= 10;
                } while (decTentaConverte % 1 != 0);
                lngDividendo = ((long)decTentaConverte);
                lngDivisor = intI;
                Simplificador();
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// funçao multíplicadora de 2 fracoes
        /// </summary>
        /// <param name="pFrac1">parametros da fracao 1</param>
        /// <param name="pFrac2">parametros da fracao 2</param>
        /// <returns>retorna resultado da multiplicacao ja simplificado</returns>
        public static Fracoes Multiplica(Fracoes pFrac1, Fracoes pFrac2)
        {
            Fracoes objFraction = new Fracoes();
            objFraction.lngDivisor = pFrac1.lngDivisor * pFrac2.lngDivisor;
            objFraction.lngDividendo = pFrac1.lngDividendo * pFrac2.lngDividendo;
            objFraction.Simplificador();
            return objFraction;
        }

        /// <summary>
        /// soma duas fracoes
        /// </summary>
        /// <param name="pFrac1">fracao 1</param>
        /// <param name="pFrac2">fracao 2</param>
        /// <returns>soma as fracoes e retorna o resul ja simplificado</returns>
        public static Fracoes Soma(Fracoes pFrac1, Fracoes pFrac2)
        {
            Fracoes objFraction = new Fracoes();
            objFraction.lngDivisor = Fracoes.Mmc(pFrac1, pFrac2);
            objFraction.lngDividendo = (pFrac1.lngDividendo * (objFraction.lngDivisor / pFrac1.lngDivisor)) + (pFrac2.lngDividendo * (objFraction.lngDivisor / pFrac2.lngDivisor));
            objFraction.Simplificador();
            return objFraction;

        }

        /// <summary>
        /// subtrae 1 fracao da outra
        /// </summary>
        /// <param name="pFrac1">fracao 1</param>
        /// <param name="pFrac2">fracao 2</param>
        /// <returns>retorna subtraçao ja simplificada</returns>
        public static Fracoes Subtrai(Fracoes pFrac1, Fracoes pFrac2)
        {
            Fracoes objFraction = new Fracoes();
            objFraction.lngDivisor = Fracoes.Mmc(pFrac1, pFrac2);
            objFraction.lngDividendo = (pFrac1.lngDividendo * (objFraction.lngDivisor / pFrac1.lngDivisor)) - (pFrac2.lngDividendo * (objFraction.lngDivisor / pFrac2.lngDivisor));
            objFraction.Simplificador();
            return objFraction;
        }
        /// <summary>
        /// divide fracao 1 por 2
        /// </summary>
        /// <param name="pFrac1">fracao 1</param>
        /// <param name="pFrac2">fracao 2</param>
        /// <returns>retorna a divisao simplificado</returns>
        public static Fracoes Divsao(Fracoes pFrac1, Fracoes pFrac2)
        {

            Fracoes objFraction = new Fracoes();
            objFraction.lngDivisor = pFrac1.lngDivisor * pFrac2.lngDividendo;
            objFraction.lngDividendo = pFrac2.lngDivisor * pFrac1.lngDividendo;
            objFraction.Simplificador();
            return objFraction;
        }

        /// <summary>
        /// tira o mmc de duas fracoes
        /// </summary>
        /// <param name="pFrac1">fracao 1</param>
        /// <param name="pFrac2">fracao 2</param>
        /// <returns>retorna o mmc dos divisores</returns>
        public static long Mmc(Fracoes pFrac1, Fracoes pFrac2)
        {
            long[] lngCadeia1 = new long[11];
            long[] lngCadeia2 = new long[11];
            long lngRecebeVal = pFrac1.lngDivisor * pFrac2.lngDivisor;
            for (int intI = 1; intI < 11; intI++)
            {
                lngCadeia1[intI] = pFrac1.lngDivisor * intI;
                lngCadeia2[intI] = pFrac2.lngDivisor * intI;
            }
            for (int intA = 1; intA < 11; intA++)
            {
                for (int intB = 1; intB < 11; intB++)
                {
                    if (lngCadeia1[intA] == lngCadeia2[intB] && lngCadeia1[intA] != 0 && lngCadeia2[intB] != 0)
                    {
                        return lngCadeia1[intA];
                    }
                }
            }
            return lngRecebeVal;
        }
    }
}

