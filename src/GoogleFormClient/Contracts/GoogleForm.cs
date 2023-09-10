namespace GoogleFormClient.Contracts;

/// <summary>
/// Google form information.
/// </summary>
public class GoogleForm
{
    /// <summary>
    /// The google form name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The google form questions.
    /// </summary>
    public List<GoogleFormQuestion> Questions { get; set; } = new();
}