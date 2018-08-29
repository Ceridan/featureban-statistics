using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class Board
    {
        private readonly BacklogColumn _backlog = new BacklogColumn();
        private readonly List<WipColumn> _wips = new List<WipColumn>();
        private readonly DoneColumn _done = new DoneColumn();

        public IReadOnlyList<WipColumn> Wips => _wips.AsReadOnly();

        public Board(int limit)
        {
            _wips.AddRange(new []
            {
                new WipColumn { Number = 1, Limit = limit },
                new WipColumn { Number = 2, Limit = limit },
            });
        }
    }
}