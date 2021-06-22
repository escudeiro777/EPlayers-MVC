using System.Collections.Generic;
using EPlayersMVC.Models;

namespace EPlayers_MVC.Interfaces
{

    namespace E_Players_AspNETCore.Interfaces
    {
        public interface IJogador
        {
            void Criar(Jogador j);

            List<Jogador> LerTodos();

            void Alterar(Jogador j);

            void Deletar(int id);
        }
    }
}