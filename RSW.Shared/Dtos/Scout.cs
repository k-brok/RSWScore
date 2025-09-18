using RSW.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Dto
{
    public class ScoutDto : BaseEntityDto
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        public required Guid PatrolId { get; set; }
    }
    public static class ScoutExtensions
    {
        public static ScoutDto ToDto(this Scout scout)
        {
            return new ScoutDto
            {
                Id = scout.Id,
                Firstname = scout.Firstname,
                Lastname = scout.Lastname,
                DateOfBirth = scout.DateOfBirth,
                IsPL = scout.IsPL,
                IsAPL = scout.IsAPL,
                PatrolId = scout.PatrolId
            };
        }

        public static Scout ToEntity(this ScoutDto scout)
        {
            return new Scout
            {
                Id = scout.Id,
                Firstname = scout.Firstname,
                Lastname = scout.Lastname,
                DateOfBirth = scout.DateOfBirth,
                IsPL = scout.IsPL,
                IsAPL = scout.IsAPL,
                PatrolId = scout.PatrolId
            };
        }
    }
}
