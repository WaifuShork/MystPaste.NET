﻿using System.Threading.Tasks;
using MystPaste.NET.Models;
using Newtonsoft.Json;

namespace MystPaste.NET
{
    public class PasteClient : ApiClient
    {
        public PasteClient(ApiRequester apiRequester) : base(apiRequester)
        { }
        
        /// <summary>
        /// Gets a paste by its id.
        /// <seealso cref="GetAuthenticatedPasteAsync(string, string)"/>
        /// </summary>
        /// <param name="pasteId">The id of the paste to get.</param>
        /// <returns>A <see cref="Paste"/> object.</returns>
        public Task<Paste> GetPasteAsync(string pasteId)
        {
            return ApiRequester.Get<Paste>(ApiUrls.GetPaste(pasteId));
        }

        /// <summary>
        /// Get a private paste via id. requires auth.
        /// <seealso cref="GetPasteAsync"/>
        /// </summary>
        /// <param name="pasteId">The id of the paste to get.</param>
        /// <param name="auth">
        /// An optional authentication token.
        /// Is only optional when a token has not been passed to the <see cref="MystPasteClient"/>.
        /// </param>
        /// <returns>A <see cref="Paste"/> object.</returns>
        /// <exception cref="InvalidAuthException">Throws when an auth token has not been passed to the client or the method.</exception>
        public Task<Paste> GetAuthenticatedPasteAsync(string pasteId, string auth = null)
        {
            auth ??= ApiRequester.Auth;
            if (auth is null)
                throw new InvalidAuthException(nameof(auth));

            return ApiRequester.Get<Paste>(ApiUrls.GetPaste(pasteId), auth);
        }
        
        public Task PostPasteAsync(PasteForm pasteForm)
        {
            return ApiRequester.Post(ApiUrls.PostPaste(),JsonConvert.SerializeObject(pasteForm));
        }

        public Task DeletePostAsync(string pasteId, string auth = null)
        {
            auth ??= ApiRequester.Auth;
            if (auth is null)
                throw new InvalidAuthException(nameof(auth));

            return ApiRequester.Delete(ApiUrls.DeletePost(pasteId), auth);
        }

        public Task EditPostAsync(string pasteId, PasteEditBuilder editBuilder, string auth = null)
        {
            auth ??= ApiRequester.Auth;
            if (auth is null)
                throw new InvalidAuthException(nameof(auth));

            return ApiRequester.Patch<Paste>(ApiUrls.EditPost(pasteId), JsonConvert.SerializeObject(editBuilder), auth);
        }
    }
}