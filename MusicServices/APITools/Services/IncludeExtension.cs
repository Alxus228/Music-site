using MusicRepository;
using MusicRepository.Interfaces;
using MusicRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicServices.APITools.Services
{
    public static class IncludeExtension
    {
        public static readonly IUserRepository _userRepository;
        public static readonly ITagRepository _tagRepository;
        public static readonly ITrackRepository _trackRepository;
        public static readonly IGenreRepository _genreRepository;
        public static readonly ICommentRepository _commentRepository;

        static IncludeExtension()
        {
            _userRepository = (IUserRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserRepository));
            _tagRepository = (ITagRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ITagRepository));
            _trackRepository = (ITrackRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ITrackRepository));
            _genreRepository = (IGenreRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IGenreRepository));
            _commentRepository = (ICommentRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ICommentRepository));
        }

        public static async Task<DomainModel.User> Include(this DomainModel.User domainUser)
        {
            var sqlUser = domainUser.ToSql();

            domainUser.FavoriteTracks = (await _trackRepository.GetTracksByUserWhoFavorite(sqlUser)).ToDomain();
            domainUser.CreatedTracks = (await _trackRepository.GetTracksByUserWhoCreated(sqlUser)).ToDomain();
            domainUser.CreatedComments = (await _commentRepository.GetCommentsByUserWhoCreated(sqlUser)).ToDomain();
            return domainUser;
        }

        public static async Task<ICollection<DomainModel.User>> Include(this ICollection<DomainModel.User> domainUsers)
        {
            List<DomainModel.User> includedDomainUsers = new List<DomainModel.User>();
            foreach(var domainUser in domainUsers)
            {
                includedDomainUsers.Add(await domainUser.Include());
            }

            return includedDomainUsers;
        }

        public static async Task<DomainModel.Comment> Include(this DomainModel.Comment domainComment)
        {
            var sqlComment = domainComment.ToSql();

            domainComment.UserWhoCreated = (await _userRepository.GetUserWhoCreatedComment(sqlComment)).ToDomain();
            domainComment.Track = (await _trackRepository.GetTrackByComment(sqlComment)).ToDomain();
            return domainComment;
        }

        public static async Task<ICollection<DomainModel.Comment>> Include(this ICollection<DomainModel.Comment> domainComments)
        {
            List<DomainModel.Comment> includedDomainComments = new List<DomainModel.Comment>();
            foreach (var domainComment in domainComments)
            {
                includedDomainComments.Add(await domainComment.Include());
            }

            return includedDomainComments;
        }

        public static async Task<DomainModel.Genre> Include(this DomainModel.Genre domainGenre)
        {
            var sqlGenre = domainGenre.ToSql();

            domainGenre.Tracks = (await _trackRepository.GetTracksByGenre(sqlGenre)).ToDomain();
            return domainGenre;
        }

        public static async Task<ICollection<DomainModel.Genre>> Include(this ICollection<DomainModel.Genre> domainGenres)
        {
            List<DomainModel.Genre> includedDomainGenres = new List<DomainModel.Genre>();
            foreach (var domainGenre in domainGenres)
            {
                includedDomainGenres.Add(await domainGenre.Include());
            }

            return includedDomainGenres;
        }

        public static async Task<DomainModel.Tag> Include(this DomainModel.Tag domainTag)
        {
            var sqlTag = domainTag.ToSql();

            domainTag.Tracks = (await _trackRepository.GetTracksByTag(sqlTag)).ToDomain();
            return domainTag;
        }

        public static async Task<ICollection<DomainModel.Tag>> Include(this ICollection<DomainModel.Tag> domainTags)
        {
            List<DomainModel.Tag> includedDomainTags = new List<DomainModel.Tag>();
            foreach (var domainTag in domainTags)
            {
                includedDomainTags.Add(await domainTag.Include());
            }

            return includedDomainTags;
        }

        public static async Task<DomainModel.Track> Include(this DomainModel.Track domainTrack)
        {
            var sqlTrack = domainTrack.ToSql();

            domainTrack.FavoriteCount = await _userRepository.GetCountWhoFavoriteTrack(sqlTrack);
            domainTrack.Tags = (await _tagRepository.GetTagsByTrack(sqlTrack)).ToDomain();
            domainTrack.Comments = (await _commentRepository.GetCommentsByTrack(sqlTrack)).ToDomain();
            domainTrack.Genre = (await _genreRepository.GetGenreByTrack(sqlTrack)).ToDomain();
            domainTrack.UserWhoCreated = (await _userRepository.GetUserWhoCreatedTrack(sqlTrack)).ToDomain();
            domainTrack.AudioFile = await _trackRepository.GetAudioFile((int)domainTrack.AudioFileId);
            return domainTrack;
        }

        public static async Task<ICollection<DomainModel.Track>> Include(this ICollection<DomainModel.Track> domainTracks)
        {
            List<DomainModel.Track> includedDomainTracks = new List<DomainModel.Track>();
            foreach (var domainTrack in domainTracks)
            {
                includedDomainTracks.Add(await domainTrack.Include());
            }

            return includedDomainTracks;
        }
    }
}