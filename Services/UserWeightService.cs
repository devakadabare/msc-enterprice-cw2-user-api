using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.DTO;
using UserApi.Entities;

namespace UserApi.Services
{
    public class UserWeightService
    {
        private ApplicationDbContext _context;

        public UserWeightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserWeightModel> GetUserWeight(int id)
        {
            var userWeight = await _context.UserWeightsLogs
                .Where(uw => uw.UserId == id)
                .OrderByDescending(uw => uw.Id)
                .FirstOrDefaultAsync();

            return userWeight;
        }

        public async Task<IEnumerable<UserWeightModel>> GetAllUserWeights(int id)
        {
            var userWeights = await _context.UserWeightsLogs
                .Where(uw => uw.UserId == id)
                .OrderByDescending(uw => uw.Id)
                .ToListAsync();

            return userWeights;
        }

        public async Task<bool> UpdateUserWeight(int id, UserWeightModel userWeight)
        {
            if (id != userWeight.UserId)
            {
                return false;
            }

            _context.Entry(userWeight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWeightExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<UserWeightModel> CreateUserWeight(UserWeightModel userWeight)
        {
            _context.UserWeightsLogs.Add(userWeight);
            await _context.SaveChangesAsync();
            return userWeight;
        }


        public async Task<UserWeightDto> CreateWeightLogAsync(WeightLogCreationDTO weightLog)
        {
            var newWeightLog = new UserWeightModel
            {
                UserId = weightLog.UserId,
                Weight = weightLog.Weight,
                Date = weightLog.Date
            };

            _context.UserWeightsLogs.Add(newWeightLog);
            await _context.SaveChangesAsync();

            var weightLogDto = new UserWeightDto
            {
                Id = newWeightLog.Id,
                UserId = newWeightLog.UserId,
                Weight = newWeightLog.Weight,
                Date = newWeightLog.Date
            };

            return weightLogDto;
        }

        public async Task<UserWeightDto> GetLatestWeightLogAsync(int userId)
        {
            var latestWeightLog = await _context.UserWeightsLogs
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.Id)
                .FirstOrDefaultAsync();

            if (latestWeightLog != null)
            {
                return new UserWeightDto
                {
                    Id = latestWeightLog.Id,
                    UserId = latestWeightLog.UserId,
                    Weight = latestWeightLog.Weight,
                    Date = latestWeightLog.Date
                };
            }

            // Handle the case where there are no weight logs for the user. 
            // You may want to throw an exception or return null.
            return null;
        }

        public async Task<bool> DeleteUserWeight(int id)
        {
            var userWeight = await _context.UserWeightsLogs.FindAsync(id);
            if (userWeight == null)
            {
                return false;
            }

            _context.UserWeightsLogs.Remove(userWeight);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool UserWeightExists(int id)
        {
            return _context.UserWeightsLogs.Any(e => e.UserId == id);
        }
    }
}
