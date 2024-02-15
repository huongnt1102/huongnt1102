using DevExpress.Data.Filtering;
using DevExpress.Utils;

namespace lichBaoTri.SearchControlClientImplementation {
    public class SchedulerSearchProvider : SearchControlProviderBase {
        protected override void DisposeCore() {
        }
        protected override SearchInfoBase GetCriteriaInfoCore(SearchControlQueryParamsEventArgs args) {
            return new SchedulerSearchInfo(args.SearchText, args.FilterCondition);
        }
    }

    public class SchedulerSearchInfo : SearchInfoBase {
        string text;
        public SchedulerSearchInfo(string t, FilterCondition condition) {
            text = t;
            FilterCondition = condition;
        }
        public override string SearchText {
            get {
                return text;
            }
        }
        public FilterCondition FilterCondition {
            get;
            set;
        }
    }
}
