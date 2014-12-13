using System;
using Windows.UI.Xaml.Data;

namespace GitHub.Converters
{
    public class EventStringToTextStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string eventType = value as string;

            switch (eventType)
            {
                case "CommitCommentEvent":
                    return "commented a commit on";
                case "CreateEvent":
                    return "created an object (repo/branch/tag) on";
                case "DeleteEvent":
                    return "deleted a branch/tag on";
                case "DeploymentEvent":
                    return "deployed";
                case "DeploymentStatusEvent":
                    throw new NotSupportedException(string.Format("The {0} is not supported.", eventType));
                case "DownloadEvent":
                    return "created a download of";
                case "FollowEvent":
                    return "started to follow";
                case "ForkEvent":
                    return "forked";
                case "ForkApplyEvent":
                    return "applied a fork on";
                case "GistEvent":
                    return "created or updated the gist";
                case "GollumEvent":
                    return "created or updated wiki page of";
                case "IssueCommentEvent":
                    return "commented an issue on";
                case "IssuesEvent":
                    return "managed an issue on";
                case "MemberEvent":
                    return "added a collaborator to";
                case "MembershipEvent":
                    return "added or removed a user from";
                case "PageBuildEvent":
                    throw new NotSupportedException(string.Format("The {0} is not supported.", eventType));
                case "PublicEvent":
                    return "made open source";
                case "PullRequestEvent":
                    return "managed pull request on";
                case "PullRequestReviewCommentEvent":
                    return "commented on pull request of";
                case "PushEvent":
                    return "pushed to";
                case "ReleaseEvent":
                    return "published a release of";
                case "RepositoryEvent":
                    return "created repository";
                case "StatusEvent":
                    throw new NotSupportedException(string.Format("The {0} is not supported.", eventType));
                case "TeamAddEvent":
                    return "added to team the repository";
                case "WatchEvent":
                    return "starred";
            }

            throw new NotSupportedException(string.Format("The {0} is not supported.", eventType));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
