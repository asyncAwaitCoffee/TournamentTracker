﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonsFile = "PersonModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> persons = PersonsFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if (persons.Count > 0)
            {
                currentId = persons.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            persons.Add(model);

            persons.SaveToPersonsFile(PersonsFile);

            return model;
        }

        //TODO - implement CreatePrize method
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes =  PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            // Get next Id
            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            // Add the new record with the new Id
            prizes.Add(model);

            // Convert the prizes to Lisr<string>
            // Save the List<string> ti the text file

            prizes.SaveToPrizesFile(PrizesFile);

            return model;

        }

        public List<PersonModel> GetPerson_All()
        {
            return PersonsFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
    }
}
