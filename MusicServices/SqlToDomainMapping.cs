using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices
{
    public static class SqlToDomainMapping
    {
        public static DomainModel.User ToDomain(this SqlModel.User sqlUser)
        {
            if (sqlUser == null) return null;

            var domainUser = new DomainModel.User
            {
                Id = sqlUser.Id,
                NickName = sqlUser.NickName,
                Email = sqlUser.Email,
                Password = sqlUser.Password,
                RoleType = (DomainModel.Role)sqlUser.RoleType,
                DateOfRegistration = sqlUser.DateOfRegistration,
                Avatar = sqlUser.Avatar
            };

            return domainUser;
        }

        public static DomainModel.Track ToDomain(this SqlModel.Track sqlTrack)
        {
            if (sqlTrack == null) return null;

            var domainTrack = new DomainModel.Track
            {
                Id = sqlTrack.Id,
                Name = sqlTrack.Name,
                Author = sqlTrack.Author,
                GenreId = sqlTrack.GenreId,
                Year = sqlTrack.Year,
                Lyrics = sqlTrack.Lyrics,
                SizeInMB = sqlTrack.SizeInMB,
                DurationInSeconds = sqlTrack.DurationInSeconds,
                Quality = sqlTrack.Quality,
                Icon = sqlTrack.Icon,
                AudioFileId = sqlTrack.AudioFileId,
                DateOfCreation = sqlTrack.DateOfCreation,
                UserIdWhoCreated = sqlTrack.UserIdWhoCreated
            };

            return domainTrack;
        }

        public static DomainModel.Tag ToDomain(this SqlModel.Tag sqlTag)
        {
            if (sqlTag == null) return null;

            var domainTag = new DomainModel.Tag
            {
                Id = sqlTag.Id,
                Name = sqlTag.Name
            };

            return domainTag;
        }

        public static DomainModel.Genre ToDomain(this SqlModel.Genre sqlGenre)
        {
            if (sqlGenre == null) return null;

            var domainGenre = new DomainModel.Genre
            {
                Id = sqlGenre.Id,
                Name = sqlGenre.Name
            };

            return domainGenre;
        }

        public static DomainModel.Comment ToDomain(this SqlModel.Comment sqlComment)
        {
            if (sqlComment == null) return null;

            var domainComment = new DomainModel.Comment
            {
                Id = sqlComment.Id,
                Content = sqlComment.Content,
                DateOfCreation = sqlComment.DateOfCreation,
                TrackId = sqlComment.TrackId,
                UserIdWhoCreated = sqlComment.UserIdWhoCreated
            };

            return domainComment;
        }

        public static ICollection<DomainModel.User> ToDomain(this ICollection<SqlModel.User> sqlUsers)
        {
            return sqlUsers.Select(ToDomain).ToList();
        }

        public static ICollection<DomainModel.Track> ToDomain(this ICollection<SqlModel.Track> sqlTracks)
        {
            return sqlTracks.Select(ToDomain).ToList();
        }

        public static ICollection<DomainModel.Tag> ToDomain(this ICollection<SqlModel.Tag> sqlTags)
        {
            return sqlTags.Select(ToDomain).ToList();
        }

        public static ICollection<DomainModel.Genre> ToDomain(this ICollection<SqlModel.Genre> sqlGenres)
        {
            return sqlGenres.Select(ToDomain).ToList();
        }

        public static ICollection<DomainModel.Comment> ToDomain(this ICollection<SqlModel.Comment> sqlComments)
        {
            return sqlComments.Select(ToDomain).ToList();
        }

    }
}
