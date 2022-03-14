using System.Diagnostics;
using Common.Core;

namespace Common.Benchmark
{
	public class StatsRecorder<TGameState>
		where TGameState : IGameState<TGameState>
	{
		private Stopwatch _stopwatch = new Stopwatch();

		private StatsResult<TGameState> _results = new StatsResult<TGameState>();
		
		public void TrainingStarted()
		{
			_stopwatch.Start();
		}

		public void TrainingFinished()
		{
			_stopwatch.Stop();
			_results.TrainingTime = _stopwatch.Elapsed.TotalSeconds;
			_stopwatch.Reset();
		}

		public void NewEvaluation()
		{
			_results.Evaluations++;
			_results.Iterations.Add(0);
		}

		public void NewIteration()
		{
			_results.Iterations[_results.Iterations.Count - 1]++;
		}

		public void ActionPlayed(AGameAction<TGameState> action)
		{
			_results.ActionsPlayed.TryGetValue(action, out long actionExecutionCount);
			_results.ActionsPlayed[action] = actionExecutionCount + 1;

			_results.TotalActionsPlayed++;
		}

		public void DeclareStuck()
		{
			_results.IsStuck = true;
		}

		public StatsResult<TGameState> GetResult()
		{
			return _results;
		}
	}
}