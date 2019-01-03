using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices
{
    public static class DomainToSqlMapping
    {
        public static SqlModel.User ToSql(this DomainModel.User domainUser)
        {
            if (domainUser == null) return null;

            var sqlUser = new SqlModel.User
            {
                Id = domainUser.Id,
                NickName = domainUser.NickName,
                Email = domainUser.Email,
                Password = domainUser.Password,
                RoleType = (int)domainUser.RoleType,
                DateOfRegistration = domainUser.DateOfRegistration,
                Avatar = domainUser.Avatar,
                IsDeleted = false
            };

            return sqlUser;
        }

        public static SqlModel.Track ToSql(this DomainModel.Track domainTrack)
        {
            if (domainTrack == null) return null;

            var sqlTrack = new SqlModel.Track
            {
                Id = domainTrack.Id,
                Name = domainTrack.Name,
                Author = domainTrack.Author,
                GenreId = domainTrack.GenreId,
                Year = domainTrack.Year,
                Lyrics = domainTrack.Lyrics,
                SizeInMB = domainTrack.SizeInMB,
                DurationInSeconds = domainTrack.DurationInSeconds,
                Quality = domainTrack.Quality,
                Icon = domainTrack.Icon,
                AudioFileId = domainTrack.AudioFileId,
                DateOfCreation = domainTrack.DateOfCreation,
                UserIdWhoCreated = domainTrack.UserIdWhoCreated,
                IsDeleted = false
            };

            return sqlTrack;
        }
        public static SqlModel.Tag ToSql(this DomainModel.Tag domainTag)
        {
            if (domainTag == null) return null;

            var sqlTag = new SqlModel.Tag
            {
                Id = domainTag.Id,
                Name = domainTag.Name,
                IsDeleted = false
            };

            return sqlTag;
        }

        public static SqlModel.Genre ToSql(this DomainModel.Genre domainGenre)
        {
            if (domainGenre == null) return null;

            var sqlGenre = new SqlModel.Genre
            {
                Id = domainGenre.Id,
                Name = domainGenre.Name,
                IsDeleted = false
            };

            return sqlGenre;
        }

        public static SqlModel.Comment ToSql(this DomainModel.Comment domainComment)
        {
            if (domainComment == null) return null;

            var sqlComment = new SqlModel.Comment
            {
                Id = domainComment.Id,
                Content = domainComment.Content,
                DateOfCreation = domainComment.DateOfCreation,
                TrackId = domainComment.TrackId,
                UserIdWhoCreated = domainComment.UserIdWhoCreated
            };

            return sqlComment;
        }

        public static ICollection<SqlModel.User> ToSql(this ICollection<DomainModel.User> domainUsers)
        {
            return domainUsers.Select(ToSql).ToList();
        }

        public static ICollection<SqlModel.Track> ToSql(this ICollection<DomainModel.Track> domainTracks)
        {
            return domainTracks.Select(ToSql).ToList();
        }

        public static ICollection<SqlModel.Tag> ToSql(this ICollection<DomainModel.Tag> domainTags)
        {
            return domainTags.Select(ToSql).ToList();
        }

        public static ICollection<SqlModel.Genre> ToSql(this ICollection<DomainModel.Genre> domainGenres)
        {
            return domainGenres.Select(ToSql).ToList();
        }

        public static ICollection<SqlModel.Comment> ToSql(this ICollection<DomainModel.Comment> domainComments)
        {
            return domainComments.Select(ToSql).ToList();
        }
    }
}
