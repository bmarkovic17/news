namespace NewsApp.Core.Domain.Entities.ArticleEntity;

/// <summary>
/// Represents the possible states of an article in the publishing lifecycle.
/// </summary>
public enum ArticleStatus
{
    /// <summary>
    /// Article is in draft state and not yet published.
    /// </summary>
    Draft,
    
    /// <summary>
    /// Article is published and publicly available.
    /// </summary>
    Published,
    
    /// <summary>
    /// Article has been archived and is no longer actively displayed.
    /// </summary>
    Unpublished
}
