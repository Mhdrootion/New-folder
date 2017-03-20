using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.WordPress;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using JamiaNadwiyya.Navigation;
using JamiaNadwiyya.ViewModels;

namespace JamiaNadwiyya.Sections
{
    public class EventsSection : Section<WordPressSchema>
    {
		private WordPressDataProvider _dataProvider;

		public EventsSection()
		{
			_dataProvider = new WordPressDataProvider();
		}

		public override async Task<IEnumerable<WordPressSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new WordPressDataConfig
            {
                QueryType = WordPressQueryType.Posts,
                Query = @"jamianadwiyyacollageevents.wordpress.com",
				FilterBy = @"",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<WordPressSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<WordPressSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<WordPressSchema>
                {
                    Title = "Events",

                    Page = typeof(Pages.EventsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.EventsDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<WordPressSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, WordPressSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Author.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<WordPressSchema>>
                {
                    ActionConfig<WordPressSchema>.Link("Go To Source", (item) => item.FeedUrl.ToSafeString()),
                };

                return new DetailPageConfig<WordPressSchema>
                {
                    Title = "Events",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
