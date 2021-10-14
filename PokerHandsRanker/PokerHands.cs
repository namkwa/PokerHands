using System;
using System.Collections.Generic;
using PokerHandsRanker.Interfaces;

namespace PokerHandsRanker
{
    public class PokerHands : IPokerHands
    {
        private readonly IDeckService _deckService;
        private readonly IHandPrinterService _handPrinterService;
        private readonly IHandRankerService _handRankerService;

        public PokerHands(IHandPrinterService handPrinterService, 
            IDeckService deckService, 
            IHandRankerService handRankerService)
        {
            _handPrinterService = handPrinterService;
            _deckService = deckService;
            _handRankerService = handRankerService;
        }

        public void Rank()
        {
            var gameOver = false;
            while (!gameOver)
            {
                Console.WriteLine("How much players ?");
                int numberOfPlayers = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("How much decks ?");
                int numberOfDecks = Convert.ToInt32(Console.ReadLine());
                var hands = new List<List<string>>();

                
                if(numberOfPlayers <= 8 && numberOfPlayers > 0 && numberOfDecks > 0 && numberOfDecks <= 4)
                {
                    var deck = _deckService.InitDeck(numberOfDecks);
                    for(int i = 0; i < numberOfPlayers; i++)
                    {
                        hands.Add(new List<string>());
                        while (hands[i].Count != 5)
                            {
                                _deckService.DrawCard(hands[i], deck);
                            }
                    }


                    var rankHand = _handRankerService.RankHand(hands[0]);
                    _handPrinterService.PrintHand(1, hands[0], rankHand);
                    var winner = 1;
                    for(int i = 0; i < numberOfPlayers-1; i++)
                    {   
                        rankHand = _handRankerService.RankHand(hands[i+1]);
                        _handPrinterService.PrintHand(i + 2, hands[i+1], rankHand);
                        if (_handRankerService.RankHands(hands[i], hands[i + 1]) != winner && _handRankerService.RankHands(hands[i], hands[i + 1])!= 0)
                        {
                            winner = _handRankerService.RankHands(hands[i], hands[i+1]) + i;
                        }
                        else
                        {
                            winner = 0;
                        }
                            
                    }
                

                    

                    Console.WriteLine(winner != 0 ? $"Player {winner} won this round !" : "It's a tie !");
                }
                else
                {
                    Console.WriteLine("incorrect number of players or decks");
                }
                Console.WriteLine("Play another hand ? Or press 'q' to quit...");
                if (Console.ReadKey().KeyChar.Equals('q'))
                {
                    gameOver = true;
                }
                Console.Clear();
            }
        }
    }
}