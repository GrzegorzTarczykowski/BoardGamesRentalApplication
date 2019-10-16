﻿using BoardGamesRentalApplication.BLL.IService;
using BoardGamesRentalApplication.DAL.Abstraction;
using BoardGamesRentalApplication.DAL.Models;
using System.Linq;

namespace BoardGamesRentalApplication.BLL.Service
{
    public class BoardGamesService : IBoardGamesService
    {
        private readonly IRepository<BoardGame> boardGameRepository;

        public BoardGamesService(IRepository<BoardGame> boardGameRepository)
        {
            this.boardGameRepository = boardGameRepository;
        }

        public void AddBoardGame(BoardGame boardGame)
        {
            boardGameRepository.Add(boardGame);
            boardGameRepository.SaveChanges();
        }

        public BoardGame FindById(int id)
        {
            return boardGameRepository.FindById(id);
        }

        public IQueryable<BoardGame> GetAll()
        {
            return boardGameRepository.GetAll(nameof(BoardGame.BoardGamePublisher)
                                            , nameof(BoardGame.BoardGameState));
        }

        public IQueryable<BoardGame> GetFourRecommendedBoardGames()
        {
            return boardGameRepository.GetAll().Take(4);
        }

        public void UpdateBoardGame(int id, BoardGame boardGame)
        {
            BoardGame edited = boardGameRepository.FindById(id);
            if (edited != null)
            {
                edited.Name = boardGame.Name;
                edited.Description = boardGame.Description;
                edited.Content = boardGame.Content;
                edited.BoardGameStateId = boardGame.BoardGameStateId;
                edited.BoardGamePublisherId = boardGame.BoardGamePublisherId;
                edited.MinimumAge = boardGame.MinimumAge;
                edited.PlayerCount = boardGame.PlayerCount;

                boardGameRepository.Edit(edited);
                boardGameRepository.SaveChanges();
            }
        }
    }
}