using System;

namespace Common
{
    public abstract class AGameAction<TGameState>
        where TGameState : ICloneable
    {
        /// <summary>
        /// Met à jour l'état de jeu fourni en exécutant une action
        /// </summary>
        /// <param name="state">Etat de jeu à mettre à jour</param>
        public abstract TGameState Apply(TGameState state);
    }
}
