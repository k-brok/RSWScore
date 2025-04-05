using System.Xml.Linq;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSW.WebApp.Helpers
{
    public class GenerateExampleData
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly IPatrolRepository _patrolRepository;
        public GenerateExampleData(IAssociationRepository associationRepository, IGroupRepository groupRepository, ICategoryRepository categoryRepository, IEditionRepository editionRepository, IPatrolRepository patrolRepository)
        {
            _associationRepository = associationRepository;
            _groupRepository = groupRepository;
            _categoryRepository = categoryRepository;
            _editionRepository = editionRepository;
            _patrolRepository = patrolRepository;
        }
        public async Task GenerateSampleDataAsync()
        {
            await GenerateCriteria();
            await GenerateEditionsAsync();
            await GenerateAssociations();
            await GeneratePatrols();
        }
        public async Task GenerateAssociations()
        {

            var associationsData = new List<(string Name, string Abbreviation, List<string> Groups)>
            {
                ("Andreas Zijlmans Groep", "AZG", new List<string> { "Batavieren", "Clemens", "Dione", "Oda" }),
                ("Maurice Flacard", "MF", new List<string> { "Scouts" }),
                ("Garcia Moreno", "GM", new List<string> { "Scouts" }),
                ("Monsieur Bekkers Groep", "MBG", new List<string> { "Scouts" }),
                ("St' Jan", "SJ", new List<string> { "Scouts" }),
                ("Martin Luther King", "MLK", new List<string> { "Scouts" }),
                ("Pastoor Siemons Groep", "PSG", new List<string> { "Scouts" }),
                ("Jan de Rooij", "JDR", new List<string> { "Verkenners", "Gidsen" }),
                ("Docter Akkermans Groep", "DAG", new List<string> { "Verkenners", "Gidsen" })
            };

            foreach (var (name, abbreviation, groups) in associationsData)
            {
                Association NewAssocitation = await _associationRepository.GetByNameAsync(name);
                if (NewAssocitation == null)
                {
                    NewAssocitation = new Association
                    {
                        Name = name,
                        Abbreviation = abbreviation,
                    };
                }

                await _associationRepository.Save(NewAssocitation);

                foreach (string GroupName in groups)
                {
                    Group NewGroup = await _groupRepository.GetByNameAsync(GroupName,NewAssocitation);

                    if (NewGroup == null)
                    {
                        NewGroup = new Group
                        {
                            Name = GroupName,
                            Association = NewAssocitation
                        };
                    }
                    await _groupRepository.Save(NewGroup);


                }
            }

            var All = await _associationRepository.GetAllAsync();
            Console.WriteLine(All.Count);
        }
        public async Task GenerateEditionsAsync()
        {

            Edition Edition2027 = new Edition();
            DateOnly RSW2027;
            
            if (DateOnly.TryParseExact("17-04-2027","dd-MM-yyyy", out RSW2027))
            {
                Edition2027.RSWStartDate = RSW2027;

                Edition2027.SubGroups.Add(new SubGroup
                {
                    Color = "Rood"
                });
                Edition2027.SubGroups.Add(new SubGroup
                {
                    Color = "Groen"
                });
                Edition2027.SubGroups.Add(new SubGroup
                {
                    Color = "Blauw"
                });
                Edition2027.SubGroups.Add(new SubGroup
                {
                    Color = "Geel"
                });
                if (await _editionRepository.GetByYearAsync(Edition2027.RSWStartDate.Year) == null)
                {
                    await _editionRepository.Save(Edition2027);
                }
            }

            Edition Edition2026 = new Edition();
            DateOnly RSW2026;

            if (DateOnly.TryParseExact("18-04-2026", "dd-MM-yyyy", out RSW2026))
            {
                Edition2026.RSWStartDate = RSW2026;

                Edition2026.SubGroups.Add(new SubGroup
                {
                    Color = "Rood"
                });
                Edition2026.SubGroups.Add(new SubGroup
                {
                    Color = "Groen"
                });
                Edition2026.SubGroups.Add(new SubGroup
                {
                    Color = "Blauw"
                });
                Edition2026.SubGroups.Add(new SubGroup
                {
                    Color = "Geel"
                });

                if (await _editionRepository.GetByYearAsync(Edition2026.RSWStartDate.Year) == null)
                {
                    await _editionRepository.Save(Edition2026);
                }
            }

            Edition Edition2025 = new Edition();
            DateOnly RSW2025;

            if (DateOnly.TryParseExact("12-04-2025", "dd-MM-yyyy", out RSW2025))
            {
                Edition2025.RSWStartDate = RSW2025;

                Edition2025.SubGroups.Add(new SubGroup
                {
                    Color = "Rood"
                });
                Edition2025.SubGroups.Add(new SubGroup
                {
                    Color = "Groen"
                });
                Edition2025.SubGroups.Add(new SubGroup
                {
                    Color = "Blauw"
                });
                Edition2025.SubGroups.Add(new SubGroup
                {
                    Color = "Geel"
                });

                if (await _editionRepository.GetByYearAsync(Edition2025.RSWStartDate.Year) == null)
                {
                    await _editionRepository.Save(Edition2025);
                    await _editionRepository.Activate(Edition2025);
                }
                
            }

            var All = await _editionRepository.GetAllAsync();
            Console.WriteLine(All.Count);
        }
        public async Task GenerateCriteria()
        {
            var beoordelingData = new List<(string CategoryName, List<(string SubCategoryName, List<(string DescriptionSet, int MaxScoreSet)> Criterias)> SubCategories, int weight)>
            {
                ("Kampopbouw", new List<(string, List<(string, int)>)>
                {
                    ("Algemene Indruk/Samenwerking", new List<(string, int)>
                    {
                        ("Alle scouts zijn bezig met het uitvoeren van een taak", 4),
                        ("Er is duidelijk overleg binnen de groep", 3),
                        ("Er wordt veilig de keuken en tent opgebouwd zonder valgevaar of instortingsgevaar", 2),
                        ("Algemene indruk", 4)
                    }),
                    ("Materiaalzorg", new List<(string, int)>
                    {
                        ("Materialen liggen tijdens de opbouw niet verspreid", 2),
                        ("Er wordt verantwoord omgegaan met materiaal", 2),
                        ("Er wordt niet over tent, tentzeil en bagage gelopen", 2),
                        ("De materialen liggen binnen het terrein", 2),
                        ("Algemene indruk omgaan met materiaal", 2)
                    }),
                    ("Keuken", new List<(string, int)>
                    {
                        ("De keuken is gemaakt met 9 of 10 palen", 1),
                        ("De driepoten zitten stevig vast en zijn goed geknoopt", 2),
                        ("Alle sjorringen zijn stevig vast en op de juiste wijze geknoopt", 2),
                        ("Keukenzeil staat strak gespannen", 1),
                        ("Keukenzeil overdekt de gehele zitgelegenheid", 1),
                        ("Keukenzeil hangt op een veilige hoogte", 1),
                        ("Keukenblad (rolblad) zit stevig vast", 1),
                        ("Keukenblad (rolblad) ligt recht", 1),
                        ("Alle leden kunnen zitten", 1),
                        ("Alles aan de keuken is gepionierd", 1)
                    }),
                    ("Tent", new List<(string, int)>
                    {
                        ("Tent staat strak en zonder plooien opgezet", 2),
                        ("Alle scheerlijnen staan strak gespannen", 2),
                        ("Alle haringen staan netjes zoals ze horen te staan", 2),
                        ("De palen staan allemaal recht", 1),
                        ("Ritsen kunnen helemaal dicht", 1),
                        ("Er is rekening gehouden met eventueel slecht weer", 2)
                    })
                },5),
                ("Tocht", new List<(string, List<(string, int)>)>
                {
                    ("Algemeen", new List<(string, int)>
                    {
                        ("Tocht", 200)
                    })
                },20),
                ("Spelmiddag", new List<(string, List<(string, int)>)>
                {
                    ("Algemeen", new List<(string, int)>
                    {
                        ("Spelmiddag", 200)
                    })
                },20),
                ("Uniform", new List<(string, List<(string, int)>)>
                {
                    ("Kleding en accessoires", new List<(string, int)>
                    {
                        ("Uniform aan en allemaal hetzelfde", 2),
                        ("Alle een groepsdas om met allen een dasring", 2),
                        ("Allemaal dezelfde scoutbroek of blauwe broek", 2),
                        ("Allemaal bovenkleding in of uit de broek", 2),
                        ("Algemene indruk", 3),
                        ("Alle hoofdbedekking is hetzelfde", 1)
                    })
                },5),
                ("Maaltijd", new List<(string, List<(string, int)>)>
                {
                    ("Hygiëne", new List<(string, int)>
                    {
                        ("Iedereen heeft schone handen", 2),
                        ("Er wordt gebruik gemaakt van een afvalbak/zak", 1),
                        ("Er wordt gebruik gemaakt van snijplanken", 1),
                        ("De tafel en kookplaats (gasstel) zijn schoon", 2),
                        ("Er liggen geen etensresten op de grond", 2),
                        ("Er wordt verantwoord omgegaan met materiaal", 1)
                    }),
                    ("Bereiding", new List<(string, int)>
                    {
                        ("Er worden verse groenten gebruikt", 1),
                        ("Er is duidelijk overleg binnen de groep", 3),
                        ("Brood of aardappels, pasta, rijst of peulvruchten", 1),
                        ("Groenten of fruit", 1),
                        ("Zuivel, vlees of vis", 1),
                        ("Vetten of oliën", 1),
                        ("Dranken", 1)
                    }),
                    ("Smaak", new List<(string, int)>
                    {
                        ("Eten is warm", 2),
                        ("Er zijn kruiden gebruikt", 1),
                        ("Groenten zijn gaar", 1),
                        ("Aardappels, rijst of pasta is gaar", 1),
                        ("Vlees of vis is gaar", 1),
                        ("Eten is op smaak en ziet er goed uit", 4)
                    }),
                    ("Thema", new List<(string, int)>
                    {
                        ("Er is een menukaart in thema", 2),
                        ("Passen de gerechten binnen het thema", 2),
                        ("Er is thema-aankleding", 2),
                        ("Algemene indruk", 4)
                    })
                },20),
                ("Scouting", new List<(string, List<(string, int)>)>
                {
                    ("Keuken", new List<(string, int)>
                    {
                        ("De gasfles is dichtgedraaid", 1),
                        ("Het tafelblad / rolblad is schoon", 1),
                        ("Kookplaats is schoon", 1),
                        ("De keuken staat binnen het aangegeven terreintje", 1),
                        ("De keukenkist is schoon en netjes ingeruimd", 4),
                        ("Er ligt niets los in de keuken", 1),
                        ("Er zijn geen etensresten meer in de keuken", 1)
                    }),
                    ("Tent", new List<(string, int)>
                    {
                        ("De tent staat nog strak gespannen", 1),
                        ("Alle palen en scheerlijnen staan nog recht", 1),
                        ("De tent staat binnen het aangegeven terreintje", 1),
                        ("Het grondzeil is schoon", 2),
                        ("Alle bagage is netjes opgeruimd", 2),
                        ("Iedereen kan bij zijn eigen bagage", 2),
                        ("De tent is op de juiste manier afgesloten", 1),
                        ("Er staat/ligt niets tegen het tentdoek", 1)
                    }),
                    ("Terrein", new List<(string, int)>
                    {
                        ("Het terrein is netjes opgeruimd", 3),
                        ("Er liggen geen materialen op het terrein verspreid", 1),
                        ("De humuslaag is niet (onnodig) beschadigd", 1),
                        ("Theedoek e.d. zijn netjes opgehangen", 1),
                        ("Er wordt gescheiden afval ingezameld", 3),
                        ("Het hele terrein, keuken en tent is voorbereid op evt. slecht weer", 2)
                    }),
                    ("Thema", new List<(string, int)>
                    {
                        ("Er is thema-aankleding op het gehele terrein", 2),
                        ("Er is in totaal veel aan thema-aankleding gedaan", 3),
                        ("Het thema-materiaal ziet er verzorgd en netjes uit", 3),
                        ("Algemene indruk van het gehele terrein in thema", 2)
                    })
                },25),
                ("Afbraak", new List<(string, List<(string, int)>)>
                {
                    ("Samenwerking", new List<(string, int)>
                    {
                        ("Alle scouts zijn bezig met het uitvoeren van een taak", 4),
                        ("Er is duidelijk overleg binnen de groep", 3),
                        ("Er wordt veilig afgebroken", 2),
                        ("Algemene indruk", 4)
                    }),
                    ("Tent/Keuken", new List<(string, int)>
                    {
                        ("Het materiaal wordt schoon gemaakt en opgerold", 5),
                        ("De touwen worden opgerold en netjes weggelegd", 2),
                        ("De palen worden bij elkaar gelegd of gelijk weggehaald", 1),
                        ("Bagage wordt bij elkaar gelegd", 2)
                    }),
                    ("Resultaat", new List<(string, int)>
                    {
                        ("Het terrein wordt geheel schoon opgeleverd", 4)
                    })
                },5)
            };


            foreach (var (CategoryName, SubCategories, weight) in beoordelingData)
            {
                Category NewCategory = await _categoryRepository.GetByNameAsync(CategoryName);
                if (NewCategory == null)
                {
                    NewCategory = new Category { Name = CategoryName, Weight = weight };
                }

                foreach(var (SubCategoryName, Criterias) in SubCategories)
                {
                    var NewSubCategory = NewCategory.SubCategories.FirstOrDefault(S => S.Name == SubCategoryName);
                    if(NewSubCategory == null)
                    {
                        NewSubCategory = new SubCategory { Name = SubCategoryName };
                        NewCategory.SubCategories.Add(NewSubCategory);
                    }

                    foreach(var (DescriptionSet, MaxScoreSet) in Criterias)
                    {
                        var NewCriteria = NewSubCategory.criterias.FirstOrDefault(S => S.Description == DescriptionSet);
                        if(NewCriteria == null)
                        {
                            NewCriteria = new Criteria { Description = DescriptionSet, MaxScore = MaxScoreSet };
                            NewSubCategory.criterias.Add(NewCriteria);
                        }
                    }
                }

                _categoryRepository.Save(NewCategory);
            }
            var All = await _categoryRepository.GetAllAsync();
            Console.WriteLine(All.Count);
        }
        public async Task GeneratePatrols()
        {
            var PatrolData = new List<(string Subgroup, List<(string Name, int number, string Association, string Group)> Patrols)>
            {
                ("Rood", new List<(string Name, int number, string Association, string Group)>
                {
                    ("MLK 1",1,"MLK","Scouts"),
                    ("MLK 5",2,"MLK","Scouts"),
                    ("GM 1",3,"GM","Scouts"),
                    ("Oda 1",4,"AZG","Oda"),
                    ("JDR V 1",5,"JDR","Verkenners"),
                    ("Clemens",6,"AZG","Clemens"),
                    ("PSG 1",7,"PSG","Scouts"),
                    ("MF 1",8,"MF","Scouts")
                }),
                ("Groen", new List<(string Name, int number, string Association, string Group)>
                {
                    ("MLK 2",9,"MLK","Scouts"),
                    ("MF 2",10,"MF","Scouts"),
                    ("GM 2",11,"GM","Scouts"),
                    ("Oda 2",12,"AZG","Oda"),
                    ("JDR G 1",13,"JDR","Gidsen"),
                    ("DAG G",14,"DAG","Gidsen"),
                    ("MBG 1",15,"MBG","Scouts")
                }),
                ("Blauw", new List<(string Name, int number, string Association, string Group)>
                {
                    ("MLK 3",16,"MLK","Scouts"),
                    ("MF 3",17,"MF","Scouts"),
                    ("GM 3", 18, "GM","Scouts"),
                    ("Dione",19,"AZG","Dione"),
                    ("JDR V 2",20,"JDR","Verkenners"),
                    ("MBG 2",21,"MBG","Scouts"),
                    ("PSG 2",22,"PSG","Scouts")
                }),
                ("Geel", new List<(string Name, int number, string Association, string Group)>
                {
                    ("MLK 4",23,"MLK","Scouts"),
                    ("MF 4",24,"MF","Scouts"),
                    ("Oda 1",25,"AZG","Oda"),
                    ("Batavieren",26,"AZG","Batavieren"),
                    ("JDR G 2",27,"JDR","Gidsen"),
                    ("DAG V",28,"DAG","Verkenners"),
                    ("SJ",29,"SJ","Scouts")
                }),
            };

            List<string> jongste = new List<string> { "MLK 2" };

            Edition CurrentEdition = await _editionRepository.GetByYearAsync(2025);

            foreach (var(Subgroup, Patrols) in PatrolData)
            {
                SubGroup CurrentSubGroup = CurrentEdition.SubGroups.FirstOrDefault(S => S.Color == Subgroup);

                foreach (var(Name, number, Association, Group) in Patrols)
                {
                    Association CurrentAssociation = await _associationRepository.GetByAbbAsync(Association);
                    Group CurrentGroup = CurrentAssociation.Groups.FirstOrDefault(G => G.Name == Group);

                    if (CurrentSubGroup.patrols.FirstOrDefault(P => P.Name == Name) == null)
                    {
                        Patrol NewPatrol = new Patrol();
                        NewPatrol.Name = Name;
                        NewPatrol.Group = CurrentGroup;
                        NewPatrol.SubGroup = CurrentSubGroup;
                        NewPatrol.Number = number;

                        if(jongste.Contains(Name))
                        {
                            NewPatrol.IsYoungest = true;
                        }

                        await _patrolRepository.Save(NewPatrol);

                        CurrentSubGroup.patrols.Add(NewPatrol);
                        CurrentGroup.Patrols.Add(NewPatrol);
                        await _groupRepository.Save(CurrentGroup);
                        
                    }
                }
            }

            await _editionRepository.Save(CurrentEdition);

            var AllEdition = await _editionRepository.GetAllAsync();
            var AllAssociation = await _associationRepository.GetAllAsync();
            Console.WriteLine(AllEdition.Count);
        }
    }
}
