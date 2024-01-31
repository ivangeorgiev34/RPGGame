using Microsoft.EntityFrameworkCore;
using RPGGame.Infrastructure.Constants;
using RPGGame.Infrastructure.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RPGGame.Infrastructure.Models
{
    [Comment(InfrastructureConstants.Player.PLAYER_CLASS_COMMENT)]
    public class Player
    {
        /// <summary>
        /// Player identifier
        /// </summary>
        [Key]
        [Comment(InfrastructureConstants.Player.PLAYER_ID_COMMENT)]
        public Guid Id { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_NAME_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_NAME_COMMENT)]
        [PlayerName(ErrorMessage = InfrastructureConstants.Player.PLAYER_NAME_ERROR_MESSAGE)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Player health
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_HEALTH_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_HEALTH_COMMENT)]
        public int Health { get; set; }

        /// <summary>
        /// Player mana
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_MANA_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_MANA_COMMENT)]
        public int Mana { get; set; }

        /// <summary>
        /// Player damage
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_DAMAGE_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_DAMAGE_COMMENT)]
        public int Damage { get; set; }

        /// <summary>
        /// Player strenght
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_STRENGHT_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_STRENGHT_COMMENT)]
        public int Strenght { get; set; }

        /// <summary>
        /// Player agility
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_AGILITY_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_AGILITY_COMMENT)]
        public int Agility { get; set; }

        /// <summary>
        /// Player intelligence
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_INTELLIGENCE_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_INTELLIGENCE_COMMENT)]
        public int Intelligence { get; set; }

        /// <summary>
        /// Player range
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_RANGE_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_RANGE_COMMENT)]
        public int Range { get; set; }

        /// <summary>
        /// Player date created
        /// </summary>
        [Required(ErrorMessage = InfrastructureConstants.Player.PLAYER_DATE_CREATED_REQUIRED_ERROR_MESSAGE)]
        [Comment(InfrastructureConstants.Player.PLAYER_DATE_CREATED_COMMENT)]
        public DateTime DateCreated { get; set; }

    }
}
