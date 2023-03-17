using FracoesEmpilhadas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace ProjectOutIn
{
    internal class Program
    {/// <summary>
     /// funcao principal do metodo
     /// </summary>
     /// <param name="pArgs">parametro inicial do metodo</param>
        static void Main(string[] pArgs)
        {
            string strPath = @"C:\Users\Felipet\Desktop\LinhaValores.txt";
            string strPath2 = @"C:\Users\Felipet\Desktop\Pr.txt";
            File.WriteAllText(strPath2, "");
            Process.Start(strPath);
            PilhaDeFracoes objStart1 = new PilhaDeFracoes(); // viculaçao da pilha
            string[] strNumbers = File.ReadAllLines(strPath); // fragmentador de string
            Console.WriteLine();
            int intT1 = 0;
            int intNt1 = 0;
            string strImprime;

            foreach (string strNumber in strNumbers) // para cada elemento fragamentado da stringM1 causa o abaixo
            {
                objStart1.ResetaValores();
                string[] strArray = strNumber.Split(' ');
                foreach (string strNum in strArray)
                {
                    Fracoes objFracoes = new Fracoes();
                    if (objFracoes.TransformaEmFracao(strNum))
                    {
                        intT1++;
                    }
                    else if ((strNum == "+" || strNum == "-" || strNum == "*" || strNum == "/"))
                    {
                        intNt1++;
                    }
                }
                if (intT1 > intNt1)
                {
                    bool blnEhInvalido = false;
                    foreach (string strNum in strArray)
                    {
                        Fracoes objFracoes = new Fracoes();
                        if (objFracoes.TransformaEmFracao(strNum)) // se strNumber for convertivel pra Fracoes add a pilha
                        {
                            objStart1.Push(objFracoes);
                        }
                        else if (objStart1.Numerador() >= 2)
                        {
                            Fracoes objResul;
                            switch (strNum)
                            {
                                case "+":
                                    objResul = Fracoes.Soma(objStart1.Pop(), objStart1.Pop());
                                    objStart1.Push(objResul);
                                    break;
                                case "-":
                                    objResul = Fracoes.Subtrai(objStart1.Pop(), objStart1.Pop());
                                    objStart1.Push(objResul);
                                    break;
                                case "/":
                                    {
                                        Fracoes objDiv1 = objStart1.Pop();
                                        Fracoes objDiv2 = objStart1.Pop();
                                        if (objDiv2.lngDividendo == 0 || objDiv1.lngDivisor == 0)
                                        {
                                            string strJJ = "INVALIDO" + Environment.NewLine;
                                            File.AppendAllText(strPath2, strJJ);
                                            blnEhInvalido = true;
                                            break;
                                        }
                                        else
                                        {
                                            objResul = Fracoes.Divsao(objDiv1, objDiv2);
                                            objStart1.Push(objResul);

                                        }
                                        break;
                                    }
                                case "*":
                                    objResul = Fracoes.Multiplica(objStart1.Pop(), objStart1.Pop());
                                    objStart1.Push(objResul);
                                    break;
                                default:
                                    {
                                        string strI = "INVALIDO" + Environment.NewLine;
                                        File.AppendAllText(strPath2, strI);
                                        blnEhInvalido = true;
                                        break;
                                    }
                            }
                        }
                        else if (objStart1.Numerador() < 2)
                        {
                            string strGG = "INVALIDO" + Environment.NewLine;
                            File.AppendAllText(strPath2, strGG);
                            blnEhInvalido = true;
                            break;
                        }
                    }
                    if (objStart1.Numerador() != 0 && !blnEhInvalido)
                    {
                        Fracoes objResult = objStart1.Pop();
                        strImprime = $"{objResult.lngDividendo}/{objResult.lngDivisor}" + Environment.NewLine;
                        File.AppendAllText(strPath2, strImprime);
                    }
                }
                else if (intT1 <= intNt1)
                {
                    string strGG = "INVALIDO" + Environment.NewLine;
                    File.AppendAllText(strPath2, strGG);

                }
                intNt1 = 0;
                intT1 = 0;
            }
            Process.Start(strPath2);
        }
    }
}
