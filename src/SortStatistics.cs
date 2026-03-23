public class SortStatistics
{
    public int Comparisons { get; set; } = 0;
    public int Copies { get; set; } = 0; 
    public long ExecutionTimeMs { get; set; } = 0;
    
    public void Reset()
    {
        Comparisons = 0;
        Copies = 0;
        ExecutionTimeMs = 0;
    }
}