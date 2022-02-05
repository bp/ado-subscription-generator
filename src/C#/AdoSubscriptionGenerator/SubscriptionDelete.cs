using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.FormInput;

namespace bp.AdoSubscriptionGenerator
{
	/// <summary>
	/// Adds a subscription via a service hook 
	/// </summary>
	public class SubscriptionDelete
	{
		private readonly VssConnection _connection;

		public SubscriptionDelete(VssConnection connection)
		{
			_connection = connection;
		}
		/// <summary>
		/// Removes a subscription given a set of details using the event hub triggers
		/// </summary>
		/// <returns>A guid subscription id</returns>
		public async Task<string> Delete(SubscriptionDetails details)
		{

			var serviceHooksClient = _connection.GetClient<ServiceHooksPublisherHttpClient>();

			SubscriptionsQuery query = new SubscriptionsQuery()
			{

				ConsumerId = "azureServiceBus",
				EventType = details.EventId,

				PublisherInputFilters = new InputFilter[] {

					new InputFilter {

						Conditions = new List < InputFilterCondition > () {

							new InputFilterCondition

							{

								InputId = "projectId",
								InputValue = details.ProjectId,
								Operator = InputFilterOperator.Equals,
								CaseSensitive = false

							}

						}

					}

				},

				ConsumerInputFilters = new InputFilter[] {

					new InputFilter {

						Conditions = new List < InputFilterCondition > () {

							new InputFilterCondition

							{

								InputId = "queueName",
								InputValue = details.QueueName,
								Operator = InputFilterOperator.Equals,
								CaseSensitive = false

							}

						}

					}

				}

			};

			var sub = await serviceHooksClient.QuerySubscriptionsAsync(query);
			await serviceHooksClient.DeleteSubscriptionAsync(sub.Results[0].Id);

			return sub.Results[0].Id.ToString();
		}
	}
}