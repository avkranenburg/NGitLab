﻿using System.Collections.Generic;
using System.Globalization;
using NGitLab.Models;

namespace NGitLab.Impl
{
    public class DeploymentClient : IDeploymentClient
    {
        private const string ProjectDeploymentsUrl = "/projects/{0}/deployments";
        private const string DeploymentMergeRequestsUrl = "/projects/{0}/deployments/{1}/merge_requests";

        private readonly API _api;

        public DeploymentClient(API api)
        {
            _api = api;
        }

        public IEnumerable<Deployment> Get(int projectId, DeploymentQuery query)
        {
            return Get(string.Format(ProjectDeploymentsUrl, projectId), query);
        }

        public IEnumerable<MergeRequest> GetMergeRequests(int projectId, int deploymentId)
        {
            return _api.Get().GetAll<MergeRequest>(string.Format(CultureInfo.InvariantCulture, DeploymentMergeRequestsUrl, projectId, deploymentId));
        }

        private IEnumerable<Deployment> Get(string url, DeploymentQuery query)
        {
            url = Utils.AddParameter(url, "state", query.Status);
            url = Utils.AddParameter(url, "order_by", query.OrderBy);
            url = Utils.AddParameter(url, "sort", query.Sort);
            url = Utils.AddParameter(url, "environment", query.Environment);
            url = Utils.AddParameter(url, "created_after", query.CreatedAfter);
            url = Utils.AddParameter(url, "created_before", query.CreatedBefore);
            url = Utils.AddParameter(url, "updated_after", query.UpdatedAfter);
            url = Utils.AddParameter(url, "updated_before", query.UpdatedBefore);

            return _api.Get().GetAll<Deployment>(url);
        }
    }
}
