using System;
using PigGame.lib.Enums;
using PigGame.lib.Validators;

namespace PigGame.lib.Factories
{
    public class DIceRollValidatorFactory
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public DIceRollValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        public IDiceRollValidator Create(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Normal:
                    return new SingleDiceRollValidator();
                case GameMode.TwoDice:
                    throw new NotImplementedException();
                case GameMode.BigPig:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}