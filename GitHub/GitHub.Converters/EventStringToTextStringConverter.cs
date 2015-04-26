using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace GitHub.Converters
{
    public class EventStringToTextStringConverter : IValueConverter
    {
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("ConvertersResources");


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string eventType = value as string;

            switch (eventType)
            {
                case "CommitCommentEvent":
                    return _resourceLoader.GetString("CommentedCommit");
                case "CreateEvent":
                    return _resourceLoader.GetString("CreatedObject");
                case "DeleteEvent":
                    return _resourceLoader.GetString("DeletedBranchTag");
                case "DeploymentEvent":
                    return _resourceLoader.GetString("Deployed");
                case "DeploymentStatusEvent":
                    throw new NotSupportedException(string.Format(_resourceLoader.GetString("NotSupportedEvent"), eventType));
                case "DownloadEvent":
                    return _resourceLoader.GetString("CreatedDownload");
                case "FollowEvent":
                    return _resourceLoader.GetString("StartedFollow");
                case "ForkEvent":
                    return _resourceLoader.GetString("Forked");
                case "ForkApplyEvent":
                    return _resourceLoader.GetString("AppliedFork");
                case "GistEvent":
                    return _resourceLoader.GetString("Gists");
                case "GollumEvent":
                    return _resourceLoader.GetString("WikiPage");
                case "IssueCommentEvent":
                    return _resourceLoader.GetString("CommentedIssue");
                case "IssuesEvent":
                    return _resourceLoader.GetString("ManagedIssue");
                case "MemberEvent":
                    return _resourceLoader.GetString("AddedCollaborator");
                case "MembershipEvent":
                    return _resourceLoader.GetString("AddedRemovedUser");
                case "PageBuildEvent":
                    throw new NotSupportedException(string.Format(_resourceLoader.GetString("NotSupportedEvent"), eventType));
                case "PublicEvent":
                    return _resourceLoader.GetString("MadeOpenSource");
                case "PullRequestEvent":
                    return _resourceLoader.GetString("ManagedPullRequest");
                case "PullRequestReviewCommentEvent":
                    return _resourceLoader.GetString("CommentedPullRequest");
                case "PushEvent":
                    return _resourceLoader.GetString("Pushed");
                case "ReleaseEvent":
                    return _resourceLoader.GetString("PublishedRelease");
                case "RepositoryEvent":
                    return _resourceLoader.GetString("CreatedRepository");
                case "StatusEvent":
                    throw new NotSupportedException(string.Format(_resourceLoader.GetString("NotSupportedEvent"), eventType));
                case "TeamAddEvent":
                    return _resourceLoader.GetString("AddedToTeam");
                case "WatchEvent":
                    return _resourceLoader.GetString("Starred");
            }

            throw new NotSupportedException(string.Format(_resourceLoader.GetString("NotSupportedEvent"), eventType));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
