namespace eDirectory.UnitTests.WCF
{
  using System;
  using Common.Message;
  using Common.ServiceContract;

  /// <summary>
  /// Wcf Dispatcher that delegates to the <see cref="WcfServiceHost"/> to
  /// get a Wcf Channel to execute the command
  /// </summary>
  public class WcfTestCommandDispatcher
    : ICommandDispatcher
  {
    public TResult ExecuteCommand<TService, TResult>(Func<TService, TResult> command)
      where TResult : IDtoResponseEnvelop
      where TService : class, IContract
    {
      var result = command.Invoke(WcfServiceHost.GetChannel<TService>());
      return result;
    }
  }
}