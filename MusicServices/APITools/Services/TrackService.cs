using DomainModel;
using MusicRepository.Interfaces;
using MusicRepository.Repositories;
using MusicServices.APITools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicServices.APITools.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task Create(DomainModel.Track domainTrack)
        {
            if (domainTrack != null)
            {
                await _trackRepository.Create(domainTrack.ToSql());

                var tracks = await this.GetAll();
                var track = tracks.FirstOrDefault(t => t.Name == domainTrack.Name && t.Author == domainTrack.Author);
                await _trackRepository.CreateAudioFile(track.Id, domainTrack.AudioFile);
                track.AudioFileId = await _trackRepository.SearchAudioFileByTrackId(track.Id);
                await _trackRepository.Update(track.ToSql());
            }
        }

        public async Task Delete(int? id)
        {
            var domainTrack = await this.Get(id);
            if (domainTrack != null)
            {
                await _trackRepository.DeleteById((int)id);
                if(domainTrack.AudioFileId != null)
                {
                    await _trackRepository.DeleteAudioFile((int)domainTrack.AudioFileId);
                }
            }
        }

        public async Task<DomainModel.Track> Get(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var sqlTrack = await _trackRepository.GetById((int)id);
            if (sqlTrack == null)
            {
                return null;
            }

            return sqlTrack.ToDomain();
        }

        public async Task<List<DomainModel.Track>> GetAll()
        {
            var sqlTracks = await _trackRepository.GetAll();
            if (sqlTracks == null)
            {
                return null;
            }

            var domainTracks = sqlTracks.ToDomain();

            return domainTracks.ToList();
        }

        public async Task Update(DomainModel.Track domainTrack)
        {
            if (domainTrack != null && await this.Get(domainTrack.Id) != null)
            {
                await _trackRepository.Update(domainTrack.ToSql());
            }
        }

        public async Task AddTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag)
        {
            if (domainTag != null && domainTrack != null && !(await IsExistTagTrackRelation(domainTrack, domainTag)))
            {
                await _trackRepository.AddToTrackTag(domainTrack.ToSql(), domainTag.ToSql());
            }
        }

        public async Task RemoveTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag)
        {
            if (await IsExistTagTrackRelation(domainTrack, domainTag))
            {
                await _trackRepository.RemoveFromTrackTag(domainTrack.ToSql(), domainTag.ToSql());
            }
        }

        public async Task<bool> IsExistTagTrackRelation(DomainModel.Track domainTrack, DomainModel.Tag domainTag)
        {
            if (domainTag == null || domainTrack == null)
            {
                return false;
            }
            if (await _trackRepository.CheckExistRelation(domainTrack.ToSql(), domainTag.ToSql()))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsDuplicate(string name, string author)
        {
            return (await this.GetAll())
                        .Any(t => t.Name == name && t.Author == author);
        }
    }
}
