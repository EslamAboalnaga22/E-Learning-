using ELearning.Core.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public YoutubeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetChannelVideos(string? pageToken = null, int maxResults = 20)
        {
            var youtubeServices = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyDf0105AbvHkLyBLcYckkP1KwrHX-sE830",
                ApplicationName = "MyYoutubeApp",
            });

            var serchRequest = youtubeServices.Search.List("snippet");
            serchRequest.ChannelId = "UCmYKFC-fgUNi9gmofWgNfsg";
            serchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            serchRequest.MaxResults = maxResults;
            serchRequest.PageToken = pageToken;  


            var searchResponse = await serchRequest.ExecuteAsync();

            var videoList = searchResponse.Items.Select(item => new VideosDetails
            {
                Title = item.Snippet.Title,
                Link = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset
            })
                .OrderByDescending(video => video.PublishedAt)
                .ToList();

            var response = new YoutubeResponse
            {
                Videos = videoList,
                NextPageToken = searchResponse.NextPageToken,
                PrevPageToken = searchResponse.PrevPageToken,
            };

            return Ok(response);
        }

        //private static UserCredential GetCredential(string clientSecretsPath)
        //{
        //    using var stream = new FileStream(clientSecretsPath, FileMode.Open, FileAccess.Read);
        //    var clientSecrets = GoogleClientSecrets.Load(stream).Secrets;
        //    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //        clientSecrets,
        //        new[] { YouTubeService.Scope.YoutubeReadonly, YouTubeService.Scope.Youtube },
        //        "user", CancellationToken.None).Result;
        //    return credential;
        //}
    }
}
