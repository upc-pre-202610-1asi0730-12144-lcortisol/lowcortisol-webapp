namespace LowCortisol.Application.Contexts.Monitoring.Contracts;

public class ThresholdEvaluationResult
{
    public bool IsExceeded { get; }
    public string Message { get; }

    private ThresholdEvaluationResult(bool isExceeded, string message)
    {
        IsExceeded = isExceeded;
        Message = message;
    }

    public static ThresholdEvaluationResult NotExceeded(string message = "Threshold not exceeded.")
    {
        return new ThresholdEvaluationResult(false, message);
    }

    public static ThresholdEvaluationResult Exceeded(string message)
    {
        return new ThresholdEvaluationResult(true, message);
    }
}