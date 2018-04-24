﻿using NGitLab.Models;
using System.Collections.Generic;
using System.Net;

namespace NGitLab.Impl
{
    public class TagClient : ITagClient
    {
        private readonly API _api;
        private readonly string _tagsPath;

        public TagClient(API api, string repoPath)
        {
            _api = api;
            _tagsPath = repoPath + "/tags";
        }

        public Tag Create(TagCreate tag)
        {
            return _api.Post().With(tag).To<Tag>(_tagsPath);
        }

        public void Delete(string name)
        {
            _api.Delete().Stream($"{_tagsPath}/{WebUtility.UrlEncode(name)}", x => {});
        }

        public IEnumerable<Tag> All => _api.Get().GetAll<Tag>(_tagsPath);
    }
}