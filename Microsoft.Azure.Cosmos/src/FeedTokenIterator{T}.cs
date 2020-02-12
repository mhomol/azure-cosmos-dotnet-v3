﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// ------------------------------------------------------------

namespace Microsoft.Azure.Cosmos
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Cosmos Result set iterator that keeps track of the FeedToken while transversing results.
    /// </summary>
    /// <example>
    /// Example on how to fully drain the query results.
    /// <code language="c#">
    /// <![CDATA[
    /// FeedTokenIterator<MyItem> feedIterator = this.Container.GetChangeFeedIterator<MyItem>();
    /// FeedToken lastFeedTokenState;
    /// while (feedIterator.HasMoreResults)
    /// {
    ///     FeedResponse<MyItem> response = await feedIterator.ReadNextAsync();
    ///     foreach (var item in response)
    ///     {
    ///         Console.WriteLine(item);
    ///     }
    ///     
    ///     // if saving state is needed, the FeedToken can be saved and stored
    ///     lastFeedTokenState = feedIterator.FeedToken;
    /// }
    /// ]]>
    /// </code>
    /// </example>
#if PREVIEW
    public
#else
    internal
#endif
    abstract class FeedTokenIterator<T>
    {
        /// <summary>
        /// Tells if there is more results that need to be retrieved from the service
        /// </summary>
        /// <example>
        /// Example on how to fully drain the query results.
        /// <code language="c#">
        /// <![CDATA[
        /// FeedTokenIterator<MyItem> feedIterator = this.Container.GetChangeFeedIterator<MyItem>();
        /// while (feedIterator.HasMoreResults)
        /// {
        ///     FeedResponse<MyItem> response = await feedIterator.ReadNextAsync();
        ///     foreach (var item in response)
        ///     {
        ///         Console.WriteLine(item);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public abstract bool HasMoreResults { get; }

        /// <summary>
        /// Current FeedToken for the iterator.
        /// </summary>
        /// 
        public abstract FeedToken FeedToken { get; }

        /// <summary>
        /// Tries to obtain a Continuation Token to be used in migration scenarios between a <see cref="FeedIterator"/> and a <see cref="FeedTokenIterator"/>.
        /// </summary>
        /// <remarks>
        /// The recommended approach to acquire and store the state of the Iterator is to consume <see cref="FeedTokenIterator.FeedToken"/>.
        /// </remarks>
        /// <param name="continuationToken">Obtained continuation token</param>
        /// <returns>Whether or not it was possible to obtain the continuation token.</returns>
        public abstract bool TryGetContinuationToken(out string continuationToken);

        /// <summary>
        /// Get the next set of results from the Cosmos service
        /// </summary>
        /// <param name="cancellationToken">(Optional) <see cref="CancellationToken"/> representing request cancellation.</param>
        /// <returns>A query response from cosmos service</returns>
        /// <example>
        /// Example on how to fully drain the query results.
        /// <code language="c#">
        /// <![CDATA[
        /// FeedTokenIterator<MyItem> feedIterator = this.Container.GetChangeFeedIterator<MyItem>();
        /// while (feedIterator.HasMoreResults)
        /// {
        ///     FeedResponse<MyItem> response = await feedIterator.ReadNextAsync();
        ///     foreach (var item in response)
        ///     {
        ///         Console.WriteLine(item);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public abstract Task<FeedResponse<T>> ReadNextAsync(CancellationToken cancellationToken = default);
    }
}