var _ = require('lodash');



class UndoRedoHistory {
	store;
	history = [];
	currentIndex = -1;

	init(store) {
		this.store = store;
	}

	addState(state) {
		// may be we have to remove redo steps
		if (this.currentIndex + 1 < this.history.length) {
			this.history.splice(this.currentIndex + 1);
		}
		this.history.push(state);
		this.currentIndex++;
	}

	clear() {
		this.history = [];
		this.currentIndex = -1;
	}

	undo() {
		if (this.currentIndex > 0) {
			const prevState = this.history[this.currentIndex - 1];
			// take a copy of the history state
			// because it would be changed during store mutations
			// what would corrupt the undo-redo-history
			// (same on redo)
			this.store.replaceState(_.cloneDeep(prevState));
			this.currentIndex--;
		}
	}

	redo() {
		if ((this.currentIndex +1 ) < this.history.length) {
			const nextState = this.history[this.currentIndex + 1];
			this.store.replaceState(_.cloneDeep(nextState));
			this.currentIndex++;
		}
	}
}

const undoRedoHistory = new UndoRedoHistory();

export default undoRedoHistory;