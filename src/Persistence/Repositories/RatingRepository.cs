namespace Persistence.Repositories;

public class RatingRepository : IRatingRepository
{
    public async Task<bool> SubmitRating(SubmitRatingDto dto)
    {
        await using var context = new ArmutDbContext();

        var existingRating = await context.Ratings.FirstOrDefaultAsync(r => r.UserId == dto.UserId && r.ServiceProviderId == dto.ProviderId);

        if (existingRating is null)
        {
            await context.Ratings.AddAsync(
                new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = dto.UserId,
                    ServiceProviderId = dto.ProviderId,
                    Date = DateTime.UtcNow,
                    RatingNumber = dto.Rating
                });

            await context.SaveChangesAsync();

            return true;
        }

        existingRating.RatingNumber = dto.Rating;
        existingRating.Date = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<double> GetAverageRatingOfProvider(GetAverageRatingOfProviderDto dto)
    {
        await using var context = new ArmutDbContext();

        var result = await context.Ratings.Where(r => r.ServiceProviderId == dto.ProviderId).AverageAsync(r => r.RatingNumber);

        return result;
    }
}