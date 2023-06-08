using ApplicationService.DTOs;
using Data.Entities;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class GameManagementService
    {
        public List<GameDTO> Get()
        {
            List<GameDTO> gameDTOs= new List<GameDTO>();
            using(UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.GameRepository.Get())
                {

                    gameDTOs.Add(new GameDTO
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Category= item.Category,
                        PublishDate = item.PublishDate,
                        UpdatedOn = item.UpdatedOn,
                        IsActive = item.IsActive,
                        ImageUrl = item.ImageUrl,
                        Developer_Id = item.Developer_Id,
                        Corporation_Id= item.Corporation_Id,
                        
                    });
                }
            }
            return gameDTOs;
        }
        public GameDTO GetById(int id)
        {
            GameDTO gameDTO = new GameDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Game game = unitOfWork.GameRepository.GetByID(id);
                if (game != null)
                {
                    gameDTO = new GameDTO
                    {
                        Id = game.Id,
                        Title = game.Title,
                        Description = game.Description,
                        Category = game.Category,
                        PublishDate = game.PublishDate,
                        UpdatedOn = game.UpdatedOn,
                        IsActive= game.IsActive,
                        ImageUrl = game.ImageUrl,
                        Developer_Id = game.Developer_Id,
                        Corporation_Id = game.Corporation_Id,

                    };

                }
            }
            return gameDTO;
        }
        public bool Update(GameDTO gameDTO)
        {

            if (!DevCheck(gameDTO.Developer_Id))
            {
                return false;
            }

            try
            {

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Game game = unitOfWork.GameRepository.GetByID(gameDTO.Id);

                    if (game == null)
                    {
                        return false;
                    }

                    game.Title = gameDTO.Title;
                    game.Description = gameDTO.Description;
                    game.Category = gameDTO.Category;
                    game.UpdatedOn = gameDTO.UpdatedOn;
                    game.IsActive= gameDTO.IsActive;
                    game.ImageUrl = gameDTO.ImageUrl;
                    game.Developer_Id = gameDTO.Developer_Id;
                    game.Corporation_Id = gameDTO.Corporation_Id;
                   

                    unitOfWork.GameRepository.Update(game);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Save(GameDTO gameDTO)
        {
            if (!DevCheck(gameDTO.Developer_Id))
            {
                return false;
            }

            Game game = new Game
            {
                Title = gameDTO.Title,
            Description = gameDTO.Description,
            Category = gameDTO.Category,
            PublishDate = gameDTO.PublishDate,
            UpdatedOn = gameDTO.UpdatedOn,
            IsActive = gameDTO.IsActive,
            ImageUrl = gameDTO.ImageUrl,
            Developer_Id = gameDTO.Developer_Id,
            Corporation_Id = gameDTO.Corporation_Id,
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.GameRepository.Insert(game);
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                Console.WriteLine(game);
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Game game = unitOfWork.GameRepository.GetByID(id);
                    unitOfWork.GameRepository.Delete(game);
                    unitOfWork.Save();
                }
                return true;
            }
            catch { return false; }
        }

        private bool DevCheck(int id)
        {

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {

                    Developer developer = unitOfWork.DevRepository.GetByID(id);
                    if (developer == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private Game GameInfo(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {

                    Game game = unitOfWork.GameRepository.GetByID(id);
                    if (game != null)
                    {
                        return game;
                    }
                    return null;

                }
            }
            catch
            {
                return null;
            }
        }

    }
}
