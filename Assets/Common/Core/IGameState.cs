using Common.Enumeration;
using System;

namespace Common.Core
{
    /// <summary>
    /// Représente un état de jeu copiable
    /// </summary>
    /// <typeparam name="TDerived">Etat de jeu à copier</typeparam>
    public interface IGameState<TDerived> : IEquatable<TDerived>
    {
        /// <summary>
        /// Clone l'état de jeu
        /// </summary>
        /// <returns>Etat de jeu cloné</returns>
        public TDerived Copy();

        /// <summary>
        /// Retourne le statut de l'état de jeu actuel
        /// </summary>
        /// <returns>Le statut de l'état de jeu</returns>
        public GameStatus Status { get; }
    }
}