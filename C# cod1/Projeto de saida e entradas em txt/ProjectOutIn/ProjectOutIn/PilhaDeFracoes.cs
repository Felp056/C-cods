using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FracoesEmpilhadas
{
    public class PilhaDeFracoes
    {
        /// <summary>
        /// add pilha para adicionar valores
        /// </summary>
        private List<Fracoes> lstPilhaList = new List<Fracoes>();

        /// <summary>
        /// Da im pop na pilha
        /// </summary>
        /// <returns>retira valor da pilha</returns>
        public Fracoes Pop()
        {
            return Pop(lstPilhaList);
        }

        /// <summary>
        /// define que da pilha é retirado um valor e reduz o contador da  pilha
        /// </summary>
        /// <param name="pIlhaList">parametro da pilha iniciada</param>
        /// <returns>funcao retira valores da pilha</returns>
        public Fracoes Pop(List<Fracoes> pIlhaList)
        {
            if (pIlhaList.Count == 0)
            {
                Console.WriteLine("Pilha vazia");
                throw new Exception();
            }
            Fracoes objNum0 = pIlhaList[pIlhaList.Count - 1];
            pIlhaList.RemoveAt(pIlhaList.Count - 1);
            return objNum0;
        }
        /// <summary>
        /// adiciona valores a pilha
        /// </summary>
        /// <param name="pValorP1">parametro da pilha</param>
        public void Push(Fracoes pValorP1)
        {
            lstPilhaList.Add(pValorP1);
        }
        /// <summary>
        /// gera um contador com o tamanho da pilha
        /// </summary>
        /// <returns>tamanho da pilha</returns>
        public int Numerador()
        {
            return lstPilhaList.Count;
        }
        /// <summary>
        /// reseta o valor da pilha
        /// </summary>
        public void ResetaValores()
        {
            lstPilhaList.Clear();
        }
    }
}
